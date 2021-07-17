var TimeObjectUtil;
/**
 * @title 时间工具类
 * @note 本类一律违规验证返回false
 * @author {boonyachengdu@gmail.com}
 * @date 2013-07-01
 * @formatter "2013-07-01 00:00:00" , "2013-07-01"
 */
TimeObjectUtil = {
    /**
     * 获取当前时间毫秒数
     */
    getCurrentMsTime: function () {
        var myDate = new Date();
        return myDate.getTime();
    },
    /**
     * 毫秒转时间格式
     */
    longMsTimeConvertToDateTime: function (time) {
        var myDate = new Date(time);
        return this.formatterDateTime(myDate);
    },
    /**
     * 时间格式转毫秒
     */
    dateToLongMsTime: function (date) {
        var myDate = new Date(date);
        return myDate.getTime();
    },
    /**
     * 格式化日期（不含时间）
     */
    formatterDate: function (date) {
        var datetime = date.getFullYear()
            + "-"// "年"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "月"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                .getDate());
        return datetime;
    },
    /**
     * 格式化日期（含时间"00:00:00"）
     */
    formatterDate2: function (date) {
        var datetime = date.getFullYear()
            + "-"// "年"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "月"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                .getDate()) + " " + "00:00:00";
        return datetime;
    },
    /**
     * 格式化去日期（含时间）
     */
    formatterDateTime: function (date) {
        var datetime = date.getFullYear()
            + "-"// "年"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "月"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                .getDate())
            + " "
            + (date.getHours() < 10 ? "0" + date.getHours() : date
                .getHours())
            + ":"
            + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date
                .getMinutes())
            + ":"
            + (date.getSeconds() < 10 ? "0" + date.getSeconds() : date
                .getSeconds());
        return datetime;
    },
    /**
     * 时间比较{结束时间大于开始时间}
     */
    compareDateEndTimeGTStartTime: function (startTime, endTime) {
        return ((new Date(endTime.replace(/-/g, "/"))) > (new Date(
            startTime.replace(/-/g, "/"))));
    },
    /**
     * 验证开始时间合理性{开始时间不能小于当前时间{X}个月}
     */
    compareRightStartTime: function (month, startTime) {
        var now = formatterDayAndTime(new Date());
        var sms = new Date(startTime.replace(/-/g, "/"));
        var ems = new Date(now.replace(/-/g, "/"));
        var tDayms = month * 30 * 24 * 60 * 60 * 1000;
        var dvalue = ems - sms;
        if (dvalue > tDayms) {
            return false;
        }
        return true;
    },
    /**
     * 验证开始时间合理性{结束时间不能小于当前时间{X}个月}
     */
    compareRightEndTime: function (month, endTime) {
        var now = formatterDayAndTime(new Date());
        var sms = new Date(now.replace(/-/g, "/"));
        var ems = new Date(endTime.replace(/-/g, "/"));
        var tDayms = month * 30 * 24 * 60 * 60 * 1000;
        var dvalue = sms - ems;
        if (dvalue > tDayms) {
            return false;
        }
        return true;
    },
    /**
     * 验证开始时间合理性{结束时间与开始时间的间隔不能大于{X}个月}
     */
    compareEndTimeGTStartTime: function (month, startTime, endTime) {
        var sms = new Date(startTime.replace(/-/g, "/"));
        var ems = new Date(endTime.replace(/-/g, "/"));
        var tDayms = month * 30 * 24 * 60 * 60 * 1000;
        var dvalue = ems - sms;
        if (dvalue > tDayms) {
            return false;
        }
        return true;
    },
    /**
     * 获取最近几天[开始时间和结束时间值,时间往前推算]
     */
    getRecentDaysDateTime: function (day) {
        var daymsTime = day * 24 * 60 * 60 * 1000;
        var yesterDatsmsTime = this.getCurrentMsTime() - daymsTime;
        var startTime = this.longMsTimeConvertToDateTime(yesterDatsmsTime);
        var pastDate = this.formatterDate2(new Date(startTime));
        var nowDate = this.formatterDate2(new Date());
        var obj = {
            startTime: pastDate,
            endTime: nowDate
        };
        return obj;
    },
    /**
     * 获取今天[开始时间和结束时间值]
     */
    getTodayDateTime: function () {
        var daymsTime = 24 * 60 * 60 * 1000;
        var tomorrowDatsmsTime = this.getCurrentMsTime() + daymsTime;
        var currentTime = this.longMsTimeConvertToDateTime(this.getCurrentMsTime());
        var termorrowTime = this.longMsTimeConvertToDateTime(tomorrowDatsmsTime);
        var nowDate = this.formatterDate2(new Date(currentTime));
        var tomorrowDate = this.formatterDate2(new Date(termorrowTime));
        var obj = {
            startTime: nowDate,
            endTime: tomorrowDate
        };
        return obj;
    },
    /**
     * 获取明天[开始时间和结束时间值]
     */
    getTomorrowDateTime: function () {
        var daymsTime = 24 * 60 * 60 * 1000;
        var tomorrowDatsmsTime = this.getCurrentMsTime() + daymsTime;
        var termorrowTime = this.longMsTimeConvertToDateTime(tomorrowDatsmsTime);
        var theDayAfterTomorrowDatsmsTime = this.getCurrentMsTime() + (2 * daymsTime);
        var theDayAfterTomorrowTime = this.longMsTimeConvertToDateTime(theDayAfterTomorrowDatsmsTime);
        var pastDate = this.formatterDate2(new Date(termorrowTime));
        var nowDate = this.formatterDate2(new Date(theDayAfterTomorrowTime));
        var obj = {
            startTime: pastDate,
            endTime: nowDate
        };
        return obj;
    }
}; 

/* 日期解析，字符串转日期
* @param dateString 可以为2017- 02 - 16，2017 / 02 / 16，2017.02.16 
* @returns { Date } 返回对应的日期对象
*/  
function dateParse(dateString) {
    var SEPARATOR_BAR = "-";
    var SEPARATOR_SLASH = "/";
    var SEPARATOR_DOT = ".";
    var dateArray;
    if (dateString.indexOf(SEPARATOR_BAR) > -1) {
        dateArray = dateString.split(SEPARATOR_BAR);
    } else if (dateString.indexOf(SEPARATOR_SLASH) > -1) {
        dateArray = dateString.split(SEPARATOR_SLASH);
    } else {
        dateArray = dateString.split(SEPARATOR_DOT);
    }
    return new Date(dateArray[0], dateArray[1] - 1, dateArray[2]);
};

/** 
 * 日期比较大小 
 * compareDateString大于dateString，返回1； 
 * 等于返回0； 
 * compareDateString小于dateString，返回-1 
 * @param dateString 日期 
 * @param compareDateString 比较的日期 
 */
function dateCompare(dateString, compareDateString) {
    if (isEmpty(dateString)) {
        alert("dateString不能为空");
        return;
    }
    if (isEmpty(compareDateString)) {
        alert("compareDateString不能为空");
        return;
    }
    var dateTime = dateParse(dateString).getTime();
    var compareDateTime = dateParse(compareDateString).getTime();
    if (compareDateTime > dateTime) {
        return 1;
    } else if (compareDateTime == dateTime) {
        return 0;
    } else {
        return -1;
    }
};

/** 
 * 判断日期是否在区间内，在区间内返回true，否返回false 
 * @param dateString 日期字符串 
 * @param startDateString 区间开始日期字符串 
 * @param endDateString 区间结束日期字符串 
 * @returns {Number} 
 */
function isDateBetween(dateString, startDateString, endDateString) {
    if (isEmpty(dateString)) {
        alert("dateString不能为空");
        return;
    }
    if (isEmpty(startDateString)) {
        alert("startDateString不能为空");
        return;
    }
    if (isEmpty(endDateString)) {
        alert("endDateString不能为空");
        return;
    }
    var flag = false;
    var startFlag = (dateCompare(dateString, startDateString) < 1);
    var endFlag = (dateCompare(dateString, endDateString) > -1);
    if (startFlag && endFlag) {
        flag = true;
    }
    return flag;
};

/** 
 * 判断日期区间[startDateCompareString,endDateCompareString]是否完全在别的日期区间内[startDateString,endDateString] 
 * 即[startDateString,endDateString]区间是否完全包含了[startDateCompareString,endDateCompareString]区间 
 * 在区间内返回true，否返回false 
 * @param startDateString 新选择的开始日期，如输入框的开始日期 
 * @param endDateString 新选择的结束日期，如输入框的结束日期 
 * @param startDateCompareString 比较的开始日期 
 * @param endDateCompareString 比较的结束日期 
 * @returns {Boolean} 
 */
function isDatesBetween(startDateString, endDateString,
    startDateCompareString, endDateCompareString) {
    if (isEmpty(startDateString)) {
        alert("startDateString不能为空");
        return;
    }
    if (isEmpty(endDateString)) {
        alert("endDateString不能为空");
        return;
    }
    if (isEmpty(startDateCompareString)) {
        alert("startDateCompareString不能为空");
        return;
    }
    if (isEmpty(endDateCompareString)) {
        alert("endDateCompareString不能为空");
        return;
    }
    var flag = false;
    var startFlag = (dateCompare(startDateCompareString, startDateString) < 1);
    var endFlag = (dateCompare(endDateCompareString, endDateString) > -1);
    if (startFlag && endFlag) {
        flag = true;
    }
    return flag;
};

function timerange(nowTime,beginTime, endTime) {
    var strb = beginTime.split(":");
   if (strb.length != 2) {
       return false;
    }
    var stre = endTime.split(":");
    if (stre.length != 2) {
        return false;
    }
    var strn = nowTime.split(":");
    if (strn.length != 2) {
        return false;
    }
     var b = new Date();
     var e = new Date();
     var n = new Date();
     b.setHours(strb[0]);
     b.setMinutes(strb[1]);

    e.setHours(stre[0]);
    e.setMinutes(stre[1]);

    n.setHours(strn[0]);
    n.setMinutes(strn[1]);
    if (e.getTime() - n.getTime() > 0 && n.getTime() - b.getTime()>0) {
       return true;
    } else {
       return false;
    }
}
//时间天数差
function DateDisparityDay(data1,data2)
{
    try {
     var date3 = new Date(date2.replace(/-/g, "/")).getTime()-new Date(date1.replace(/-/g, "/")).getTime();//时间差的毫秒数   
    //计算出相差天数  
    var days = Math.floor(date3/(24*3600*1000));
    return days;
    } catch (e) {
        alert(e.message);
    }
};