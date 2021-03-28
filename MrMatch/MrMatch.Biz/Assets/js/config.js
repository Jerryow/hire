window.api = {
    passport: "/api/passportapi/",
    account: "/api/accountapi/",
    candidate: "/api/candidateapi/",
    invite: "/api/inviteapi/",
    interview: "/api/interviewapi/",
    share: "/api/shareapi/",
    delivery: "/api/jobdeliveryapi/",
    message: "/api/MessageApi/"
};

window.axiosPost = function (url, paras) {
    return new Promise((resolve, reject) => {
        axios({
            url: url,
            method: 'post',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            data: paras
        }).then(res => {
            resolve(res.data);
        }).catch(function (error) {
            //console.log(error);
            reject(error);
        });
    });
};

window.axiosGet = function (url, paras) {
    return new Promise((resolve, reject) => {
        axios({
            url: url,
            method: 'get',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            params: paras
        }).then(res => {
            resolve(res.data);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
};

window.axiosGetAsync = function (url, paras) {
    return new Promise((resolve, reject) => {
        axios({
            url: url,
            method: 'get',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            params: paras
        }).then(res => {
            resolve(res.data);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
};

window.pageFill = function (index, size, keywords) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords
    };
};

window.pageFillWithPKID = function (index, size, keywords, pkid) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords,
        "PKID": pkid
    };
};

window.getUrlKey = function (name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.href) || [, ""])[1].replace(/\+/g, '%20')) || null;
};
window.getFullUrl = function () {
    return document.location;
};

window.getDateTimeNow = function () {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return year + "-" + month + "-" + day;
};