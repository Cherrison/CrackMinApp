# CrackMinApp
   **2.0版本今日更新，可以直接解压在任何地方使用，无需指定**
  
   ## [Bug最新修复时间 2019.4.15 bug修复请看最下方](#jump)

一键获取微信小程序源码, 使用了C#加nodejs制作
直接解压在D盘根目录下后就可以使用
将小程序文件放到 wxapkg目录下
这个目录下有一些demo 可以先进行实验
然后打开 CrackMinApp.exe 按说明即可使用
# CrackMinApp是C#方面的源代码, nodejs已经配置好安装好依赖文件无需改动
### 本工具界面效果
![Image text](https://img-blog.csdnimg.cn/20190312102443109.jpg?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxOTg0NjM0,size_16,color_FFFFFF,t_70)
 
那么如何才能在手机里找到小程序的源文件包呢？
具体目录位置直接给出：
/data/data/com.tencent.mm/MicroMsg//appbrand/pkg/
在这个目录下，会发现一些 xxxxxxx.wxapkg 类型的文件，这些就是微信小程序的包

微信小程序的格式就是:.wxapkg
.wxapkg是一个二进制文件，有其自己的一套结构。
关于.wxapkg的详细内容可以参考lrdcq大神的博文：http://lrdcq.com/me/read.php/66.htm

这里我提供两种方法 

## 一.安卓手机有root

  安卓re管理器 进入
  /data/data/com.tencent.mm/MicroMsg//appbrand/pkg/
  然后就会发现一些wxapkg后缀的文件
  这些文件 当你打开一个新的微信小程序就会生成 如果不知道是那一个 可以现在这个目录下删除所有文件
  然后打开你想要反编译的小程序, 新出现的wxapkg文件 就是你想要的

至于如何root请自行查找

## 二.使用安卓模拟器获取到.wxapkg文件

  不用越狱，不用root，使用电脑端的安卓模拟器来获取是一个非常简单快捷且万能的获取方式，具体步骤如下：

  打开安装好的安卓模拟器，并在模拟器中安装QQ、微信、RE管理器
  QQ、微信在模拟器自带的应用商店里搜索下载安装即可
  QQ、微信在模拟器自带的应用商店里搜索下载安装即可
  RE管理器的下载地址自行百度
  下载好后直接拖拽进打开的模拟器窗口就会自动安装
  设置一下模拟器
  以我个人认为比较好用的夜神模拟器举例
  首先到模拟器内部设置超级用户权限
  ![Image text](http://meetes.top/images/categories/wechat/2018/06/1.jpeg)
  ![Image text](http://meetes.top/images/categories/wechat/2018/06/2.jpeg)
  
  这些操作的目的都是为了能让RE管理器顺利的获取到ROOT权限
  接下来在模拟器里打开微信，然后在微信中运行你想要获取的下程序（这其实是让微信把小程序的源文件包从服务器下载到了本地了）
  举个例子: 
  在模拟器微信中运行一下后，直接切回模拟器桌面运行RE浏览器 来到目录
  /data/data/com.tencent.mm/MicroMsg//appbrand/pkg/
  就抵达了目的文件夹  
  
  你会看到发现里面的一些.wxapkg后缀的文件，就是它们没错啦，可以根据使用的时间来判断那个是你刚才从服务器下载过来的
一般小程序的文件不会太大，可以结合时间来判断，长按压缩所选文件,然后再将压缩好的包通过QQ发送到我的电脑
如果不进行压缩的话，是无法将这个文件通过QQ来发送的
所以QQ的这个功能可以让我们很方便的拿到源文件，而不必到电脑目录去找模拟器的文件目录。
解压。这样几步简单操作，就成功拿到了小程序的源文件了。

最终我们就得到了我们想要的小程序的源代码

原参考链接: https://blog.csdn.net/qq_33858250/article/details/80543815
http://lrdcq.com/me/read.php/66.htm

# <span id = "jump">Bug修复</span>
# 2019.3.22
## 修复无法执行问题
### nodejs/nodejs下面这个压缩包node_modules.zip解压一下。 本来这下面是有个node_modules文件夹的可是由于github默认不上传超过100个文件导致没有上传这个关键文件 

![Image text](https://s2.ax1x.com/2019/03/24/AYZDtU.png)

### 注意未避免文件名称过长导致的错误，请把目录下文件复制到一个根目录下新建的文件夹下面

![Image text](https://s2.ax1x.com/2019/03/24/AYeyUf.png)

### 路径名称过长
# 2019.4.15

## 修复wxappUnpacker __mainPageFrameReady__ is not defined
## 修复ReferenceError: $gwx is not defined

