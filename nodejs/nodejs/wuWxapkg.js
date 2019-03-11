const wu=require("./wuLib.js");
const wuJs=require("./wuJs.js");
const wuCfg=require("./wuConfig.js");
const wuMl=require("./wuWxml.js");
const wuSs=require("./wuWxss.js");
const path=require("path");
const fs=require("fs");
function header(buf){
	console.log("\nHeader info:");
	let firstMark=buf.readUInt8(0);
	console.log("  firstMark: 0x%s",firstMark.toString(16));
	let unknownInfo=buf.readUInt32BE(1);
	console.log("  unknownInfo: ",unknownInfo);
	let infoListLength=buf.readUInt32BE(5);
	console.log("  infoListLength: ",infoListLength);
	let dataLength=buf.readUInt32BE(9);
	console.log("  dataLength: ",dataLength);
	let lastMark=buf.readUInt8(13);
	console.log("  lastMark: 0x%s",lastMark.toString(16));
	if(firstMark!=0xbe||lastMark!=0xed)throw Error("Magic number is not correct!");
	return [infoListLength,dataLength];
}
function genList(buf){
	console.log("\nFile list info:");
	let fileCount=buf.readUInt32BE(0);
	console.log("  fileCount: ",fileCount);
	let fileInfo=[],off=4;
	for(let i=0;i<fileCount;i++){
		let info={};
		let nameLen=buf.readUInt32BE(off);
		off+=4;
		info.name=buf.toString('utf8',off,off+nameLen);
		off+=nameLen;
		info.off=buf.readUInt32BE(off);
		off+=4;
		info.size=buf.readUInt32BE(off);
		off+=4;
		fileInfo.push(info);
		console.log(info);
	}
	return fileInfo;
}
function saveFile(dir,buf,list){
	console.log("Saving files...");
	for(let info of list)
		wu.save(path.resolve(dir,(info.name.startsWith("/")?".":"")+info.name),buf.slice(info.off,info.off+info.size));
}
function packDone(dir,cb,order){
	console.log("Unpack done.");
	let weappEvent=new wu.CntEvent,needDelete={};
	weappEvent.encount(4);
	weappEvent.add(()=>{
		wu.addIO(()=>{
			console.log("Split and make up done.");
			if(!order.includes("d")){
				console.log("Delete files...");
				wu.addIO(()=>console.log("Deleted.\n\nFile done."));
				for(let name in needDelete)if(needDelete[name]>=8)wu.del(name);
			}
			cb();
		});
	});
	function doBack(deletable){
		for(let key in deletable){
			if(!needDelete[key])needDelete[key]=0;
			needDelete[key]+=deletable[key];//all file have score bigger than 8 will be delete.
		}
		weappEvent.decount();
	}
	//This will be the only func running this time, so async is needless.
	if(fs.existsSync(path.resolve(dir,"app-service.js"))){//weapp
		console.log("Split app-service.js and make up configs & wxss & wxml & wxs...");
		wuCfg.doConfig(path.resolve(dir,"app-config.json"),doBack);
		wuJs.splitJs(path.resolve(dir,"app-service.js"),doBack);
		if(fs.existsSync(path.resolve(dir,"workers.js")))
			wuJs.splitJs(path.resolve(dir,"workers.js"),doBack);
		if(fs.existsSync(path.resolve(dir,"page-frame.html")))
			wuMl.doFrame(path.resolve(dir,"page-frame.html"),doBack,order);
		else if(fs.existsSync(path.resolve(dir,"app-wxss.js"))) {
			wuMl.doFrame(path.resolve(dir,"app-wxss.js"),doBack,order);
			if(!needDelete[path.resolve(dir,"page-frame.js")])needDelete[path.resolve(dir,"page-frame.js")]=8;
		} else throw Error("page-frame-like file is not found in the package by auto.");
		wuSs.doWxss(dir,doBack);//Force it run at last, becuase lots of error occured in this part
	}else if(fs.existsSync(path.resolve(dir,"game.js"))){//wegame
		console.log("Split game.js and rewrite game.json...");
		let gameCfg=path.resolve(dir,"app-config.json");
		wu.get(gameCfg,cfgPlain=>{
			let cfg=JSON.parse(cfgPlain);
			if(cfg.subContext){
				console.log("Found subContext, splitting it...")
				delete cfg.subContext;
				let contextPath=path.resolve(dir,"subContext.js");
				wuJs.splitJs(contextPath,()=>wu.del(contextPath));
			}
			wu.save(path.resolve(dir,"game.json"),JSON.stringify(cfg,null,4));
			wu.del(gameCfg);
		});
		wuJs.splitJs(path.resolve(dir,"game.js"),()=>{
			wu.addIO(()=>{
				console.log("Split and rewrite done.");
				cb();
			});
		});
	}else throw Error("This package is unrecognizable.\nMay be this package is a subPackage which should be unpacked with -s=<MainDir>.\nOtherwise, please decrypted every type of file by hand.")
}
function doFile(name,cb,order){
	for(let ord of order)if(ord.startsWith("s="))global.subPack=ord.slice(3);
	console.log("Unpack file "+name+"...");
	let dir=path.resolve(name,"..",path.basename(name,".wxapkg"));
	wu.get(name,buf=>{
		let [infoListLength,dataLength]=header(buf.slice(0,14));
		if(order.includes("o"))wu.addIO(console.log.bind(console),"Unpack done.");
		else wu.addIO(packDone,dir,cb,order);
		saveFile(dir,buf,genList(buf.slice(14,infoListLength+14)));
	},{});
}
module.exports={doFile:doFile};
if(require.main===module){
    wu.commandExecute(doFile,"Unpack a wxapkg file.\n\n[-o] [-d] [-s=<Main Dir>] <files...>\n\n-d Do not delete transformed unpacked files.\n-o Do not execute any operation after unpack.\n-s=<Main Dir> Regard all packages provided as subPackages and\n              regard <Main Dir> as the directory of sources of the main package.\n<files...> wxapkg files to unpack");
}
