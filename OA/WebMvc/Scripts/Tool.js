//miss guo 2018-04-08 add
var uncount = navigator.userAgent, app = navigator.appVersion;
var isAndroid = uncount.indexOf('Android') > -1 || uncount.indexOf('Linux') > -1; //android终端或者uc浏览器
var isiOS = !!uncount.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端