var TimeObjectUtil;
/**
 * @title ʱ�乤����
 * @note ����һ��Υ����֤����false
 * @author {boonyachengdu@gmail.com}
 * @date 2013-07-01
 * @formatter "2013-07-01 00:00:00" , "2013-07-01"
 */
TimeObjectUtil = {
    /**
     * ��ȡ��ǰʱ�������
     */
    getCurrentMsTime: function () {
        var myDate = new Date();
        return myDate.getTime();
    },
    /**
     * ����תʱ���ʽ
     */
    longMsTimeConvertToDateTime: function (time) {
        var myDate = new Date(time);
        return this.formatterDateTime(myDate);
    },
    /**
     * ʱ���ʽת����
     */
    dateToLongMsTime: function (date) {
        var myDate = new Date(date);
        return myDate.getTime();
    },
    /**
     * ��ʽ�����ڣ�����ʱ�䣩
     */
    formatterDate: function (date) {
        var datetime = date.getFullYear()
            + "-"// "��"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "��"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                .getDate());
        return datetime;
    },
    /**
     * ��ʽ�����ڣ���ʱ��"00:00:00"��
     */
    formatterDate2: function (date) {
        var datetime = date.getFullYear()
            + "-"// "��"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "��"
            + (date.getDate() < 10 ? "0" + date.getDate() : date
                .getDate()) + " " + "00:00:00";
        return datetime;
    },
    /**
     * ��ʽ��ȥ���ڣ���ʱ�䣩
     */
    formatterDateTime: function (date) {
        var datetime = date.getFullYear()
            + "-"// "��"
            + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                + (date.getMonth() + 1))
            + "-"// "��"
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
     * ʱ��Ƚ�{����ʱ����ڿ�ʼʱ��}
     */
    compareDateEndTimeGTStartTime: function (startTime, endTime) {
        return ((new Date(endTime.replace(/-/g, "/"))) > (new Date(
            startTime.replace(/-/g, "/"))));
    },
    /**
     * ��֤��ʼʱ�������{��ʼʱ�䲻��С�ڵ�ǰʱ��{X}����}
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
     * ��֤��ʼʱ�������{����ʱ�䲻��С�ڵ�ǰʱ��{X}����}
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
     * ��֤��ʼʱ�������{����ʱ���뿪ʼʱ��ļ�����ܴ���{X}����}
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
     * ��ȡ�������[��ʼʱ��ͽ���ʱ��ֵ,ʱ����ǰ����]
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
     * ��ȡ����[��ʼʱ��ͽ���ʱ��ֵ]
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
     * ��ȡ����[��ʼʱ��ͽ���ʱ��ֵ]
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

/* ���ڽ������ַ���ת����
* @param dateString ����Ϊ2017- 02 - 16��2017 / 02 / 16��2017.02.16 
* @returns { Date } ���ض�Ӧ�����ڶ���
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
 * ���ڱȽϴ�С 
 * compareDateString����dateString������1�� 
 * ���ڷ���0�� 
 * compareDateStringС��dateString������-1 
 * @param dateString ���� 
 * @param compareDateString �Ƚϵ����� 
 */
function dateCompare(dateString, compareDateString) {
    if (isEmpty(dateString)) {
        alert("dateString����Ϊ��");
        return;
    }
    if (isEmpty(compareDateString)) {
        alert("compareDateString����Ϊ��");
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
 * �ж������Ƿ��������ڣ��������ڷ���true���񷵻�false 
 * @param dateString �����ַ��� 
 * @param startDateString ���俪ʼ�����ַ��� 
 * @param endDateString ������������ַ��� 
 * @returns {Number} 
 */
function isDateBetween(dateString, startDateString, endDateString) {
    if (isEmpty(dateString)) {
        alert("dateString����Ϊ��");
        return;
    }
    if (isEmpty(startDateString)) {
        alert("startDateString����Ϊ��");
        return;
    }
    if (isEmpty(endDateString)) {
        alert("endDateString����Ϊ��");
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
 * �ж���������[startDateCompareString,endDateCompareString]�Ƿ���ȫ�ڱ������������[startDateString,endDateString] 
 * ��[startDateString,endDateString]�����Ƿ���ȫ������[startDateCompareString,endDateCompareString]���� 
 * �������ڷ���true���񷵻�false 
 * @param startDateString ��ѡ��Ŀ�ʼ���ڣ��������Ŀ�ʼ���� 
 * @param endDateString ��ѡ��Ľ������ڣ��������Ľ������� 
 * @param startDateCompareString �ȽϵĿ�ʼ���� 
 * @param endDateCompareString �ȽϵĽ������� 
 * @returns {Boolean} 
 */
function isDatesBetween(startDateString, endDateString,
    startDateCompareString, endDateCompareString) {
    if (isEmpty(startDateString)) {
        alert("startDateString����Ϊ��");
        return;
    }
    if (isEmpty(endDateString)) {
        alert("endDateString����Ϊ��");
        return;
    }
    if (isEmpty(startDateCompareString)) {
        alert("startDateCompareString����Ϊ��");
        return;
    }
    if (isEmpty(endDateCompareString)) {
        alert("endDateCompareString����Ϊ��");
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
//ʱ��������
function DateDisparityDay(data1,data2)
{
    try {
     var date3 = new Date(date2.replace(/-/g, "/")).getTime()-new Date(date1.replace(/-/g, "/")).getTime();//ʱ���ĺ�����   
    //������������  
    var days = Math.floor(date3/(24*3600*1000));
    return days;
    } catch (e) {
        alert(e.message);
    }
};