//定义一个可静态调用方法的js类
window.ChkUtil = function () {};
//校验是否为空(先删除二边空格再验证)
ChkUtil.isNull = function (str) {
    if (null == str || "" == $.trim(str)) {
        return true;
    } else {
        return false;
    }
};
//校验是否全是数字
ChkUtil.isDigit = function (str) {
    var patrn = /^\d+$/;
    return patrn.test(str);
};
//校验是否是整数
ChkUtil.isInteger = function (str) {
    var patrn = /^([+-]?)(\d+)$/;
    return patrn.test(str);
};
//校验是否为正整数
ChkUtil.isPlusInteger = function (str) {
    var patrn = /^[1-9]\d*$/;
    return patrn.test(str);
};
//校验是否为负整数
ChkUtil.isMinusInteger = function (str) {
    var patrn = /^-(\d+)$/;
    return patrn.test(str);
};
//校验是否为浮点数
ChkUtil.isFloat = function (str) {
    var patrn = /^([+-]?)\d*\.\d+$/;
    return patrn.test(str);
};
//校验是否为正浮点数
ChkUtil.isPlusFloat = function (str) {
    var patrn = /^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$/;
    return patrn.test(str);
};
//校验是否为负浮点数
ChkUtil.isMinusFloat = function (str) {
    var patrn = /^-\d*\.\d+$/;
    return patrn.test(str);
};
//校验是否为正数
ChkUtil.isPositiveNumber = function (str) {
    var patrn = /^[1-9]*[1-9][0-9]*$/;
    return patrn.test(str);
};

//校验是否仅中文
ChkUtil.isChinese = function (str) {
    var patrn = /[\u4E00-\u9FA5\uF900-\uFA2D]+$/;
    return patrn.test(str);
};
//校验是否仅ACSII字符
ChkUtil.isAcsii = function (str) {
    var patrn = /^[\x00-\xFF]+$/;
    return patrn.test(str);
};
//校验手机号码
ChkUtil.isMobile = function (str) {
    var patrn = /^1[34578]\d{9}$/;
    //var patrn = /^0?1((3[0-9]{1})|(59)){1}[0-9]{8}$/;
    return patrn.test(str);
};
//校验电话号码
ChkUtil.isPhone = function (str) {
    var patrn = /^(0[\d]{2,3}-)?\d{6,8}(-\d{3,4})?$/;
    return patrn.test(str);
};
//校验URL地址
ChkUtil.isUrl = function (str) {
    var patrn = /^http[s]?:\/\/[\w-]+(\.[\w-]+)+([\w-\.\/?%&=]*)?$/;
    return patrn.test(str);
};
//校验电邮地址
ChkUtil.isEmail = function (str) {
    var patrn = /^([a-zA-Z0-9_\.\-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+$/;
    return patrn.test(str);
};
//校验邮编
ChkUtil.isZipCode = function (str) {
    var patrn = /^\d{6}$/;
    return patrn.test(str);
};
//校验合法时间
ChkUtil.isDate = function (str) {
    if (!/\d{4}(\.|\/|\-)\d{1,2}(\.|\/|\-)\d{1,2}/.test(str)) {
        return false;
    }
    var r = str.match(/\d{1,4}/g);
    if (r == null) {
        return false;
    };
    var d = new Date(r[0], r[1] - 1, r[2]);
    return d.getFullYear() == r[0] && d.getMonth() + 1 == r[1] && d.getDate() == r[2];
};
//校验字符串：只能输入6-20个字母、数字、下划线(常用手校验用户名和密码)
ChkUtil.isString6_20 = function (str) {
    var patrn = /^(\w){6,20}$/;
    return patrn.test(str);
};
//校验字符串：身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
ChkUtil.isCard = function (str) {
    var patrn = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    return patrn.test(str);
};
//校验字符串：海关10位编码只能为数字
ChkUtil.isSeaCode = function (str) {
    var patrn = /(^\d{10}$)/;
    return patrn.test(str);
};
//校验报告编号：字母和数字的组合
ChkUtil.isReportCode = function (str) {
    var patrn = /^[0-9a-zA-Z]{1,}$/;
    return patrn.test(str);
};
//校验是否为业务编号
ChkUtil.isOrderCode = function (str, orderType) {
    var _orderCode = str.toString();
    var _orderType = orderType.toString();
    if (_orderType.indexOf('|') > -1) {
        for (var i = 0; i < _orderType.split("|").length; i++) {
            _orderCode = _orderCode.replace(_orderType.split("|")[i].toUpperCase(), "").replace(_orderType.split("|")[i].toLowerCase(), "");
        }
    }
    _orderCode = _orderCode.replace(_orderType.toUpperCase(), "").replace(_orderType.toLowerCase(), "");
    var patrn = /^\d{1,7}$/;
    return patrn.test(_orderCode);
};

ChkUtil.getTime = function () {
    return new Date().getTime();
};

ChkUtil.checkPackageSerialNo = function (serialNo) {
    var _mod = parseInt(serialNo.substring(serialNo.length - 1));

    var _sn = parseInt(serialNo.substring(0, serialNo.length - 1));

    return _sn % 5 == _mod;
};

ChkUtil.removeFromArr = function (o, val) {
    /// <summary>
    /// 从数组中移除元素
    /// </summary>
    var _isArrary = ChkUtil.isArrary(o);
    if (_isArrary) {
        var index = o.indexOf(val);
        if (index > -1) {
            o.splice(index, 1);
        }
    }
};

ChkUtil.groupArrByProp = function groupBy(array, f) {
    var _isArrary = ChkUtil.isArrary(array);
    if (_isArrary) {
        /// <summary>
        /// 数组分组
        /// </summary>
        var groups = {};
        array.forEach(function (o) {
            var group = JSON.stringify(f(o));
            groups[group] = groups[group] || [];
            groups[group].push(o);
        });
        return Object.keys(groups).map(function (group) {
            return groups[group];
        });
    }
};

ChkUtil.removeFromArrByIdx = function (o, idx) {
    /// <summary>
    /// 从数组中移除元素
    /// </summary>
    var _isArrary = ChkUtil.isArrary(o);
    if (_isArrary && idx <= o.length - 1 && idx > -1) {
        o.splice(idx, 1);
    }
};

ChkUtil.isArrary = function (o) {
    /// <summary>
    /// 是否为数组
    /// </summary>
    return Object.prototype.toString.call(o) == '[object Array]';
};

ChkUtil.fmtDT = function (fmt) {
    /// <summary>
    /// 格式化时间，参数：yyyy-MM-dd HH:mm:ss.fff
    /// </summary>
    if (fmt.length > 0) {
        var o = {
            "M+": this.getMonth() + 1, //月份
            "d+": this.getDate(), //日
            "h+": this.getHours(), //小时
            "H+": this.getHours(), //小时
            "m+": this.getMinutes(), //分
            "s+": this.getSeconds(), //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            "S": this.getMilliseconds(), //毫秒
            "f": this.getMilliseconds() //毫秒
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
    }
    return fmt;
};
ChkUtil.dateToFormat = function (value) {
    if (value == '至今') {
        return '至今';
    } else {
        var date = value.split('T')[0].split('-');
        date[1] = date[1].replace(/^0+/g, "");
        if (date.length > 2) date[2] = date[2].replace(/^0+/g, "");

        return date[0] + '.' + date[1];
    }
}, ChkUtil.compare = function (input, str, exact) {
    /// <summary>
    /// 是否为数组
    /// </summary>
    return input !== undefined && (exact ? input === str : input.match(str));
};

ChkUtil.fileType = function (vName) {
    if (ChkUtil.compare(vName, /\.(gif|png|jpe?g)$/i)) {
        return "image";
    } else if (ChkUtil.compare(vName, /\.(htm|html)$/i)) {
        return "html";
    } else if (ChkUtil.compare(vName, /\.(rtf|docx?|xlsx?|pptx?|pps|potx?|ods|odt|pages|ai|dxf|ttf|tiff?|wmf|e?ps)$/i)) {
        return "office";
    } else if (ChkUtil.compare(vName, /\.(txt|md|csv|nfo|ini|json|php|js|css)$/i)) {
        return "text";
    } else if (ChkUtil.compare(vName, /\.(og?|mp4|webm|mp?g|mov|3gp)$/i)) {
        return "video";
    } else if (ChkUtil.compare(vName, /\.(og?|mp3|mp?g|wav)$/i)) {
        return "audio";
    } else if (ChkUtil.compare(vName, /\.(swf)$/i)) {
        return "flash";
    } else if (ChkUtil.compare(vName, /\.(pdf)$/i)) {
        return "pdf";
    } else {
        return "other";
    }
};

ChkUtil.getQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return decodeURI(r[2]);
    }
    return null;
};

window.Dictionary = function () {
    /// <summary>
    /// Dictionary
    /// </summary>
    this.datastore = new Array();
    this.add = function (key, value) {
        this.datastore[key] = value;
    };
    this.find = function (key) {
        return this.datastore[key];
    };
    this.remove = function (key) {
        delete this.datastore[key];
    };
    this.showAll = function () {
        if (window.console) {
            var str = "";
            for (var key in this.datastore) {
                str += key + " -> " + this.datastore[key] + ";  ";
            }
            console.log(str);
        }
    };
    this.count = function () {
        var n = 0;
        for (var key in Object.keys(this.datastore)) {
            ++n;
        }
        return n;
    };
    this.clear = function () {
        for (var key in this.datastore) {
            delete this.datastore[key];
        }
    };
};