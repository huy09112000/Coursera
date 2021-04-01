$(document).ready(function () {
    //set up level
    let objId = $('#level').children()[2].id;
    let level = objId.split("-")[1];
    level = parseInt(level);
    let child = $('#' + objId).children();
    child.each(function (idx) {
        if (idx < level) {
            $(this).css('background', ' #BD8E02');
        }
    });

    //set up button start exam
    $('#start__exam').on("animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd", function (event) {
        //console.log(1, event.originalEvent.animationName);
        $(this).removeClass("bounding");
    });

    //set up timer
    let timerId = $('#timer').children()[1].id;
    let time = parseInt(timerId.split("-")[1]) * 60;
    let lstTime = getNumTimer(time);
    let hunded_minute = lstTime[2];
    let doze_minute = lstTime[1];
    let minute = lstTime[0];
    let dozen_second = lstTime[4];
    let second = lstTime[3];
    setTimer(hunded_minute, hunded_minute, 'hundred_minute');
    setTimer(doze_minute, doze_minute, 'dozen_minute');
    setTimer(minute, minute, 'minute');
    setTimer(dozen_second, dozen_second, 'dozen_second');
    setTimer(second, second, 'second');


    $('.main-carousel').flickity({
        // options
        cellAlign: 'left',
        contain: true,
        adaptiveHeight: true,
        wrapAround: true
    });
});

function getNumTimer(number) {
    let a = [];
    let second = -1;
    if ((number * 60) % 60 !== 0) {
        second = (number * 60) % 60;
    }
    while (number > 0) {
        a.push(number % 10);
        number = Math.floor(number / 10);
    }
    if (second !== -1) {
        while (second > 0) {
            a.push(second % 10);
            second = Math.floor(second / 10);
        }
    }
    a.push(0);
    a.push(0);
    a.push(0);
    a.push(0);
    a.push(0);
    return a;
}

function setTimer(up, down, id) {
    let data = `<span class='count curr top flipTop'> ${down} 
                </span><span class='count next top'> ${up} 
                </span><span class='count next bottom flipBottom'>${up} 
                </span><span class='count curr bottom'>${up}</span>`;
    document.getElementById(id).innerHTML = data;
}

function startExam(id) {
    $('#start__exam').on("animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd", function (event) {
        //console.log(1, event.originalEvent.animationName);
        //$(this).removeClass("bounding");
        if (event.originalEvent.animationName === "boundingOut") {
            $('.quizz__overview').children().each(function () {
                $(this).addClass('fadeOutRight');
            });
        }
    });
    $('.quizz__overview').on("animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd", function (event) {
        $(this).css('display','none');
        $('#start__exam').css('display', 'none');
        $('.main-carousel').addClass("transformVisible");
    });
    $('#start__exam').removeClass('bounding');

    $('#start__exam').addClass('boundingOut');

    HTTPGet('/quizz/exam', { quizzId: id }, initTesting)
        .done(function () { })
        .fail(function (jqxhr, setting, ex) { console.log(ex) });
}

function initTesting(res) {
    console.log(1, res);
    let data = res.data;

    for (var i = 0; i < data.length; i++) {
        let qes = data[i];
        let item = `<div class="carouser_ques" id="qes-${qes.Id}">
                <div class="num_ques">
                    <span>Question: ${i+1}</span>
                </div>
                <div class="carouser_point">
                    <span>Point: ${qes.Point}</span>
                </div>
                <div class="carouser_content">
                    <span>${qes.Content}</span>
                </div>
                <div class="carouser_answer">

                </div>
            </div>`

    }
}
function hightLight() {
    $('input[type="radio"]').parent().css('background-color', '#006699');
    $('input[type="radio"]:checked').parent().css('background-color', '#00017A');
}