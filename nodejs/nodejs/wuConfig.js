const wu=require("./wuLib.js");
const fs=require("fs");
const path=require("path");
const crypto=require("crypto");
const {VM}=require('vm2');
function getWorkerPath(name){
	let code=fs.readFileSync(name,{encoding:'utf8'});
	let commPath=false;
	let vm=new VM({sandbox:{
		require(){},
		define(name){
			name=path.dirname(name)+'/';
			if(commPath===false)commPath=name;
			commPath=wu.commonDir(commPath,name);
		}
	}});
	vm.run(code.slice(code.indexOf("define(")));
	if(commPath.length>0)commPath=commPath.slice(0,-1);
	console.log("Worker path: \""+commPath+"\"");
	return commPath;
}
function doConfig(configFile,cb){
	let dir=path.dirname(configFile);
	wu.get(configFile,content=>{
		let e=JSON.parse(content);
		let k=e.pages;
		k.splice(k.indexOf(wu.changeExt(e.entryPagePath)),1);
		k.unshift(wu.changeExt(e.entryPagePath));
		let app={pages:k,window:e.global&&e.global.window,tabBar:e.tabBar,networkTimeout:e.networkTimeout};
		if(e.subPackages){
			app.subPackages=e.subPackages;
			console.log("=======================================================\nNOTICE: SubPackages exist in this package.\nDetails: ",app.subPackages,"\n=======================================================");
		}
		if(fs.existsSync(path.resolve(dir,"workers.js")))app.workers=getWorkerPath(path.resolve(dir,"workers.js"));
		if(e.extAppid)
			wu.save(path.resolve(dir,'ext.json'),JSON.stringify({extEnable:true,extAppid:e.extAppid,ext:e.ext},null,4));
		if(typeof e.debug!="undefined")app.debug=e.debug;
		let cur=path.resolve("./file");
		for(let a in e.page)if(e.page[a].window.usingComponents)
			for(let name in e.page[a].window.usingComponents){
				let componentPath=e.page[a].window.usingComponents[name]+".html";
				let file=componentPath.startsWith('/')?componentPath.slice(1):wu.toDir(path.resolve(path.dirname(a),componentPath),cur);
				if(!e.page[file])e.page[file]={};
				if(!e.page[file].window)e.page[file].window={};
				e.page[file].window.component=true;
			}
		let delWeight=8;
		for(let a in e.page){
			let fileName=path.resolve(dir,wu.changeExt(a,".json"));
			wu.save(fileName,JSON.stringify(e.page[a].window,null,4));
			if(configFile==fileName)delWeight=0;
		}
		if(app.tabBar&&app.tabBar.list) wu.scanDirByExt(dir,"",li=>{//search all files
			let digests=[],digestsEvent=new wu.CntEvent,rdir=path.resolve(dir);
			function fixDir(dir){return dir.startsWith(rdir)?dir.slice(rdir.length+1):dir;}
			digestsEvent.add(()=>{
				for(let e of app.tabBar.list){
					e.pagePath=wu.changeExt(e.pagePath);
					if(e.iconData){
						let hash=crypto.createHash("MD5").update(e.iconData,'base64').digest();
						for(let [buf,name] of digests)if(hash.equals(buf)){
							delete e.iconData;
							e.iconPath=fixDir(name).replace(/\\/g,'/');
							break;
						}
					}
					if(e.selectedIconData){
						let hash=crypto.createHash("MD5").update(e.selectedIconData,'base64').digest();
						for(let [buf,name] of digests)if(hash.equals(buf)){
							delete e.selectedIconData;
							e.selectedIconPath=fixDir(name).replace(/\\/g,'/');
							break;
						}
					}
				}
				wu.save(path.resolve(dir,'app.json'),JSON.stringify(app,null,4));
				cb({[configFile]:delWeight});
			});
			for(let name of li){
				digestsEvent.encount();
				wu.get(name,data=>{
					digests.push([crypto.createHash("MD5").update(data).digest(),name]);
					digestsEvent.decount();
				},{});
			}
		});else{
			wu.save(path.resolve(dir,'app.json'),JSON.stringify(app,null,4));
			cb({[configFile]:delWeight});
		}
	});
}
module.exports={doConfig:doConfig};
if(require.main===module){
    wu.commandExecute(doConfig,"Split and make up weapp app-config.json file.\n\n<files...>\n\n<files...> app-config.json files to split and make up.");
}
