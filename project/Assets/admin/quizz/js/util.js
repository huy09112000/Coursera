

function hideOnClick(ele, id) {
    let s = '#' + id + 'c';
    if ($(s).hasClass("toLeftTransition") || $(s).hasClass("hide")) {
        return;
    }
    let code = id;
    let leftPos = ele.offsetLeft - 50;
    let topPos = ele.offsetTop - 50;
    ele.style.left = leftPos + 'px';
    ele.style.top = topPos + 'px';
    //ele.style.position = 'absolute';
    $('.list__item').one("transitionend webkitTransitionEnd oTransitionEnd", function () {
        if ($(s).hasClass("hide")) {
            //alert('hie');
            ele.classList.remove("hide");
        } else {
            ele.classList.add("toLeftTransition");
        }
    });
    $(".list__item").addClass("hide", 1000);
    $(".list__item").removeClass("up");

    //ele.classList.remove("hide");
    //setTimeout(function () {
    //    ele.classList.add("toLeftTransition");
    //}, 2000);

    $(".list__item").on("click", function (e) {
        e.preventDefault();
        return;
    })

    HTTPGet('/quizz/listsubject', { id: code, pageNum: 1 }, addSubject)
        .done(function () { })
        .fail(function (jqxhr, setting, ex) { console.log(ex) });
}

function addSubject(res) {
    let data = res.data;
    let courseId = res.courseId;
    let list = ``;
    for (var obj of data) {
        list += `  <div class="item__subject" onclick="window.location.href='/quizz/subject/${courseId}/${obj.Id}'">
            <div class="subject__header"><h1>${obj.Name}</h1></div>
            <div class="subject__content">
                <p>
                    ${obj.Describtion}
                </p>
            </div>
        </div>`;
    }
    $('#lstsubject').append(list);
    $('#lstsubject').on('transitionend webkitTransitionEnd oTransitionEnd', function () {
        $('.item__subject').toggleClass('grow');
    });
    $('#lstsubject').toggleClass('show');
}

//subject section
//jQuery(function ($) {
//    $('#lstsubject').on('scroll', function () {

//        $('#lstsubject').stop().animate({
//            scrollTop: $('#lstsubject').scrollTop() - (200)
//        });
//    })
//});

function wheel(event) {
    //var delta = 0;
    //if (event.wheelDelta) delta = event.wheelDelta / 120;
    //else if (event.detail) delta = -event.detail / 3;
    //handle(delta);
    //if (event.preventDefault) event.preventDefault();
    //event.returnValue = false;
}
function handle(delta) {
    var time = 1000;
    var distance = 56;
    if ($('#lstsubject').scrollHeight - $('#lstsubject').scrollTop() == $('#lstsubject').outerHeight()) {
        console.log("bottom");
    }
    $('#lstsubject').stop().animate({
        scrollTop: $('#lstsubject').scrollTop() - (distance * delta)
    });
}

//lesson section

$(document).ready(function () {
    console.log(1, $('#lstQuizz').children());
    let child = $('#lstQuizz').children();
    child.each(function (idx, val) {
        let level = val.id.split("-")[1];
        level = parseInt(level);
        let lst = $(this).find(".quizz__level").children();

        lst.each(function (childId) {
            if (childId < level) {
                $(this).css('background', ' #BD8E02');
            }
        });

    });
});
function moveToExam(ele){
    let idQuizz = ele.id.split("-")[0];
    let idLesson = ele.id.split("-")[3]
    window.location.href = "/quizz/test/" + idLesson + "/" + idQuizz;
}


