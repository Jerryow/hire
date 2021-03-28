function axiosPost(url, paras) {
    return new Promise((resolve, reject) => {
        axios({
            url: url,
            method: 'post',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            data: paras
        }).then((res) => {
            resolve(res);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
}

function axiosGet(url, paras) {
    return new Promise((resolve, reject) => {
        axios({
            url: url,
            method: 'get',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            params: paras
        }).then((res) => {
            resolve(res);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
}

function pageFill(index, size, keywords) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords
    };
}

function pageFillWithPKID(index, size, keywords, pkid) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords,
        "PKID": pkid
    };
}

function getUlrKey(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.href) || [, ""])[1].replace(/\+/g, '%20')) || null;
}

function getFullUlr() {
    return document.location;
}

function getDateTimeNow() {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return year + "-" + month + "-" + day;
}
