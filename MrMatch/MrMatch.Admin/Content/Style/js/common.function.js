//设置分页步长
function setPageSize(size) {
    _pageSize = size;
    goto(1);
}

/*搜索*/
function gosearch(key) {
    _keyWords = key.trim();
    goto(1);
}

//分页
function gopagination(pIndex, pCount) {
    //初始化
    $('#dataPagination').html('');
    _pageIndex = pIndex;
    var _left = '';
    var _middle = '';
    var _right = '';
    //有数据
    if (pCount > 0) {

        if (pIndex > pCount) {
            pIndex = pCount;
        }

        _middle += '<li ><a href="javascript:goto(' + pIndex + ');" style="background:#eee;">' + pIndex + '</a></li>'

        if (pIndex == 1) {
            _left += '<li><a href="javascript:void(0);">&laquo;</a></li>';
            if (pCount > 1) {
                _middle += '<li><a href="javascript:goto(' + (pIndex + 1) + ');">' + (pIndex + 1) + '</a></li>';
                _right += '<li><a href="javascript:goto(' + pCount + ');">&raquo;</a></li>';
            }
            else {
                _right += '<li><a href="javascript:void(0);">&raquo;</a></li>';
            }
        }
        else {
            _left += '<li><a href="javascript:goto(1);">&laquo;</a></li>';
            _left += '<li><a href="javascript:goto(' + (pIndex - 1) + ');">' + (pIndex - 1) + '</a></li>';
            if (pIndex != pCount) {
                _middle += '<li><a href="javascript:goto(' + (pIndex + 1) + ');">' + (pIndex + 1) + '</a></li>';
                _right += '<li><a href="javascript:goto(' + pCount + ');">&laquo;</a></li>';
            }
            else {
                _right += '<li><a href="javascript:void(0);">&raquo;</a></li>';
            }
        }
        $('#dataPagination').html(_left + _middle + _right);
    }
}