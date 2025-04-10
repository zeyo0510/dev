// https://app.mitrade.com/

// 上方：標題列
// 左方：選單
// 下方：熱門品種
$(".el-header.cc_el-header").style.display = "none";
$(".el-aside.aside-menu").style.display = "none";
$(".el-footer").style.display = "none";

// 隱藏浮動買賣按鈕
$(".operation-btn").style.display = "none";

// 日夜模式
// $(".clothes-wrap.clothes-padding-left")
// $(".clothes-wrap.clothes-padding-right")
$(".clothes-wrap.clothes-padding-left").getElementsByTagName("div")[0].getElementsByTagName("svg")[0].dispatchEvent(new Event('click'))
$(".clothes-wrap.clothes-padding-right").getElementsByTagName("div")[1].getElementsByTagName("svg")[0].dispatchEvent(new Event('click'))

// 全螢幕
// $(".icon.screen-icon")
$(".icon.screen-icon").dispatchEvent(new Event('click'))

// 目前價格
// $(".info_profit.animate-flipX.info_profit").textContent

// 套保
// 當營利為正時，設定止損值

// 平尾盤
// 當時間為 06:55:00 時，平倉