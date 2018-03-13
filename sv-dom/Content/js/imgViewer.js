/*
 imgViewer 1.0.0
 Licensed under the MIT license.
 https://github.com/VaJoy/imgViewer
 */
(function ($, window) {
    $.bindViewer = function (elm) {
        var $elm = $(elm),
            $win = $(window);
        $("html").css("minHeight", "100%");

        if ($elm.data("bindCategory")) $(document).off("click", elm);
        else $elm.data("bindCategory", "bound");
        $(document).on("click", elm, viewImg);

        function viewImg() {
            var $obj = $(this),
                src = $obj.attr("src-big"),
                win_h = window.innerHeight || document.documentElement.clientHeight,
                win_w = window.innerWidth || document.documentElement.clientWidth,
                sroll_t = $win.scrollTop(),
                sroll_l = $win.scrollLeft(),
                doc_h = Math.max($("body").height(), $(document).height()),
                $img = $("<img style='position:absolute;z-index:9999998;left:50%;border-radius:8px;opacity:0;' src='" + src + "' />"),
                $bg = $("<div style='position:absolute;z-index:9999997;top:0;left:0;width:100%;height:" + doc_h + "px;background:black;opacity:0.8;'></div>"),
                $close = $("<a style='position:absolute;z-index:9999999;left:50%;padding:11px 15px;cursor:pointer;background:black;color:white;border-radius:50%;font-family:Arial;font-size:14px;transition:background .5s;text-decoration:none;'>X</a>");
            $bg.appendTo("body").hide().fadeIn(200);
            $close.appendTo("body").hover(function () {
                $(this).css({"background": "#D90000", "text-decoration": "none"})
            }, function () {
                $(this).css("background", "black")
            }).hide();
            $img.appendTo("body").load(function () {
                var img_w = $(this).width(),
                    img_h = $(this).height();
                var koef = 0.95;
                if(win_h*koef<img_h||win_w*koef<img_w){ 
                    var win_scale = win_w/win_h,
                        img_scale = img_w/img_h,
                        temp = 0;
                    if(win_scale>img_scale){ 
                        temp = img_h;
                        img_h = win_h*koef;
                        img_w = img_w*img_h/temp;
                    }else{  
                        temp = img_w;
                        img_w = win_w*koef;
                        img_h = img_w*img_h/temp;
                    }
                }
                $(this).css({"top": win_h / 2 + sroll_t, "margin-left": sroll_l - 50, "margin-top": "-50px", "width": "100px", "height": "100px"})
                    .animate({"opacity": "1", "margin-left": -img_w / 2, "margin-top": -img_h / 2, "width": img_w, "height": img_h}, 300,
                    function () {
                        $close.css({"top": win_h / 2 + sroll_t, "margin-left": img_w / 2 - 20 + sroll_l, "margin-top": -15 - img_h / 2}).fadeIn(500);
                    });
                $close.add($bg).click(function () {
                    $img.add($bg).add($close).remove();
                    $img = $bg = $close = null;
                })
            })
        }
    };

}(jQuery, window));