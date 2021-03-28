
window.imgVer = function(Config) {
	var el = typeof Config.el == 'string'?eval(Config.el):Config.el;
	var w = Config.width;
	var h = Config.height;
	var imgLibrary = Config.img;
	var PL_Size = 48;
	var padding = 20;
	var MinN_X = padding + PL_Size;
	var MaxN_X = w - padding - PL_Size - PL_Size / 6;
	var MaxN_Y = padding;
	var MinN_Y = h - padding - PL_Size - PL_Size / 6;
	function RandomNum(Min, Max) {
		var Range = Max - Min;
		var Rand = Math.random();
		if (Math.round(Rand * Range) == 0) {
			return Min + 1;
		}
		else if (Math.round(Rand * Max) == Max) {
			return Max - 1;
		}
		else {
			var num = Min + Math.round(Rand * Range) - 1;
			return num;
		}
    }
    var imgRandom = RandomNum(1, imgLibrary.length);
	var imgSrc = imgLibrary[0];
    //var X = RandomNum(MinN_X, MaxN_X);
    var X = Config.offsetx
	var Y = RandomNum(MinN_Y, MaxN_Y);
	var left_Num = -X + 10;
	var html = '<div><div style="position:relative;">';
	html += '<div style="position:relative;overflow:hidden;width:' + w + 'px;">';
	html += '<div style="position:relative;width:' + w + 'px;height:' + h + 'px;">';
	html += '<img id="scream" src="' + imgSrc + '" style="width:' + w + 'px;height:' + h + 'px;">';
	html += '<canvas id="puzzleBox" width="' + w + '" height="' + h + '" style="position:absolute;left:0;top:0;z-index:9922;"></canvas>';
	html += '</div>';
	html += '<div class="puzzle-lost-box" style="position:absolute;width:' + w + 'px;height:' + h + 'px;top:0;left:' + left_Num + 'px;z-index:99111;">';
	html += '<canvas id="puzzleShadow" width="' + w + '" height="' + h + '" style="position:absolute;left:0;top:0;z-index:9922;"></canvas>';
	html += '<canvas id="puzzleLost" width="' + w + '" height="' + h + '" style="position:absolute;left:0;top:0;z-index:9933;"></canvas>';
	html += '</div>';
	html += '<p class="ver-tips"></p>';
	html += '</div>';
	html += '</div>';
	html += '<div style="position:relative;width:' + w + 'px;margin:20px auto;">';
	html += '<div style="border:1px solid #eee;border-radius:24px;background:#eee;margin:0 10px">';
	html += '<p style="font-size:12px;color:#486c80;line-height:28px;margin:0;text-align:right;padding-right:22px;">按住左边滑块，拖动完成上方拼图</p>';
	html += '</div>';
	html += '<div class="slider-btn"></div>';
	html += '</div>';
    html += '<div class="re-btn"><a class="reset" title="刷新"></a><a class="close" title="关闭"></a></div></div>';
	el.html(html);
    var d = PL_Size / 3;
    var shadow = 10
	var c = document.getElementById("puzzleBox");
	var ctx = c.getContext("2d");
	ctx.globalCompositeOperation = "xor";
    ctx.shadowBlur = shadow;
	ctx.shadowColor = "#fff";
	ctx.shadowOffsetX = 3;
	ctx.shadowOffsetY = 3;
	ctx.fillStyle = "rgba(0,0,0,0.7)";
	ctx.beginPath();
	ctx.lineWidth = "1";
	ctx.strokeStyle = "rgba(0,0,0,0)";
	ctx.moveTo(X, Y);
	ctx.lineTo(X + d, Y);
	ctx.bezierCurveTo(X + d, Y - d, X + 2 * d, Y - d, X + 2 * d, Y);
	ctx.lineTo(X + 3 * d, Y);
	ctx.lineTo(X + 3 * d, Y + d);
	ctx.bezierCurveTo(X + 2 * d, Y + d, X + 2 * d, Y + 2 * d, X + 3 * d, Y + 2 * d);
	ctx.lineTo(X + 3 * d, Y + 3 * d);
	ctx.lineTo(X, Y + 3 * d);
	ctx.closePath();
	ctx.stroke();
	ctx.fill();
	var c_l = document.getElementById("puzzleLost");
	var c_s = document.getElementById("puzzleShadow");
	var ctx_l = c_l.getContext("2d");
	var ctx_s = c_s.getContext("2d");
	var img = new Image();
	img.src = imgSrc;
	img.onload = function () {
		ctx_l.drawImage(img, 0, 0, w, h);
	}
	//console.log(X,Y)
	ctx_l.beginPath();
	ctx_l.strokeStyle = "rgba(0,0,0,0)";
	ctx_l.moveTo(X, Y);
	ctx_l.lineTo(X + d, Y);
	ctx_l.bezierCurveTo(X + d, Y - d, X + 2 * d, Y - d, X + 2 * d, Y);
	ctx_l.lineTo(X + 3 * d, Y);
	ctx_l.lineTo(X + 3 * d, Y + d);
	ctx_l.bezierCurveTo(X + 2 * d, Y + d, X + 2 * d, Y + 2 * d, X + 3 * d, Y + 2 * d);
	ctx_l.lineTo(X + 3 * d, Y + 3 * d);
	ctx_l.lineTo(X, Y + 3 * d);
	ctx_l.closePath();
	ctx_l.stroke();
	ctx_s.fill();
	ctx_l.clip();
	ctx_s.beginPath();
	ctx_s.lineWidth = "1";
	ctx_s.strokeStyle = "rgba(0,0,0,0)";
	ctx_s.moveTo(X, Y);
	ctx_s.lineTo(X + d, Y);
	ctx_s.bezierCurveTo(X + d, Y - d, X + 2 * d, Y - d, X + 2 * d, Y);
	ctx_s.lineTo(X + 3 * d, Y);
	ctx_s.lineTo(X + 3 * d, Y + d);
	ctx_s.bezierCurveTo(X + 2 * d, Y + d, X + 2 * d, Y + 2 * d, X + 3 * d, Y + 2 * d);
	ctx_s.lineTo(X + 3 * d, Y + 3 * d);
	ctx_s.lineTo(X, Y + 3 * d);
	ctx_s.closePath();
	ctx_s.stroke();
	ctx_s.shadowBlur = 12;
	ctx_s.shadowColor = "black";
	ctx_s.fill();
	var moveStart = '';
	$(".slider-btn").on('mousedown touchstart', function (e) {
		e = e || window.event;
		$(this).css({
			"background-position": "0 -216px"
		}
        );
        moveStart = e.pageX || e.originalEvent.targetTouches[0].pageX;

        //console.log('moveStart', moveStart)
	});
	$(window).on('mousemove touchmove', function (e) {
		e = e || window.event;
		var moveX = e.pageX || e.originalEvent.targetTouches[0].pageX;
		var d = moveX - moveStart;
		if (moveStart == '') {
		} else {
			if (d < 0 || d > (w - padding - PL_Size)) {
			} else {
				$(".slider-btn").css({
					"left": d + 'px', "transition": "inherit"
				}
				);
				$("#puzzleLost").css({
					"left": d + 'px', "transition": "inherit"
				}
				);
				$("#puzzleShadow").css({
					"left": d + 'px', "transition": "inherit"
				}
				);
			}
		}
	})
	$(window).on('mouseup touchend', function (e) {
		e = e || window.event;
		var e_pageX = e.originalEvent.changedTouches == undefined ? moveStart : e.originalEvent.changedTouches[0];
        var moveEnd_X = e.pageX - moveStart || e_pageX.pageX - moveStart;
		var ver_Num = X - 10;
		var deviation = parseInt(Config.voffset);
		var Min_left = ver_Num - deviation;
		var Max_left = ver_Num + deviation;
		if (moveStart == '') {
		} else {
			if (Max_left > moveEnd_X && moveEnd_X > Min_left) {
				$(".ver-tips").html('<i style="background-position:-4px -1207px;"></i><span style="color:#42ca6b;">验证通过</span><span></span>');
				$(".ver-tips").addClass("slider-tips");
				$(".puzzle-lost-box").addClass("hidden");
				$("#puzzleBox").addClass("hidden");
				setTimeout(function () {
					$(".ver-tips").removeClass("slider-tips");
					imgVer(Config);
					
					$("#imgVer .re-btn .close").click();
				}, 1000);
                Config.success({ 'moveEnd_X': moveEnd_X + shadow});
			} else {
				$(".ver-tips").html('<i style="background-position:-4px -1229px;"></i><span style="color:red;">验证失败:</span><span style="margin-left:4px;">拖动滑块将悬浮图像正确拼合</span>');
				$(".ver-tips").addClass("slider-tips");
				setTimeout(function () {
					$(".ver-tips").removeClass("slider-tips");
                }, 1000);

                shake('#imgVer>div') 

				Config.error();
			}
        }
        setTimeout(function () {
			$(".slider-btn").css({
				"left": '0', "transition": "left 0.5s"
			}
			);
			$("#puzzleLost").css({
				"left": '0', "transition": "left 0.5s"
			}
			);
			$("#puzzleShadow").css({
				"left": '0', "transition": "left 0.5s"
			}
			);
		}, 500);
		$(".slider-btn").css({
			"background-position": "0 -84px"
		});
		moveStart = '';
    })
    $(".re-btn .reset").on("click", function () {
        $(".slider-btn").off('mousedown touchstart')
        $(window).off('mousemove touchmove')
        $(window).off('mouseup touchend')

        imgVer(Config);
    })
    $(".re-btn .close").on("click", function () {
        $(".slider-btn").off('mousedown touchstart')
        $(window).off('mousemove touchmove')
        $(window).off('mouseup touchend')

        $(this).parents('.verBox').remove()
    })


    $('.verBox').css({ 'width': $(window).width(), 'height': $(window).height() })
    window.onresize = function () {
        $('.verBox').css({ 'width': $(window).width(), 'height': $(window).height() })
    }
    $('.verBox-ghost').click(function () {
        //$(".re-btn .close").click()

        shake('#imgVer>div') 
    })
}


function shake(str) {

    $(str).addClass('shake')
    setTimeout(function () {
        $(str).removeClass('shake')
    }, 500) 
}