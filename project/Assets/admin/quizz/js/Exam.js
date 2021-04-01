
const WARNING_THRESHOLD = 10;
const ALERT_THRESHOLD = 5;


const COLOR_CODES = {
    info: {
        color: "green"
    },
    warning: {
        color: "orange",
        threshold: WARNING_THRESHOLD
    },
    alert: {
        color: "red",
        threshold: ALERT_THRESHOLD
    }
};

let timerInterval = null;
let remainingPathColor = COLOR_CODES.info.color;
const FULL_DASH_ARRAY = 283;

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
    let timeLeft = $('.quizz__time').children()[1].id.split("-")[1] * 60 * 60;
    document.getElementById("clock").innerHTML = `
<div class="base-timer">
  <svg class="base-timer__svg" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
    <g class="base-timer__circle">
      <circle class="base-timer__path-elapsed" cx="50" cy="50" r="45"></circle>
      <path
        id="base-timer-path-remaining"
        stroke-dasharray="283"
        class="base-timer__path-remaining ${remainingPathColor}"
        d="
          M 50, 50
          m -45, 0
          a 45,45 0 1,0 90,0
          a 45,45 0 1,0 -90,0
        "
      ></path>
    </g>
  </svg>
  <span id="base-timer-label" class="base-timer__label">${formatTime(timeLeft)}</span>
</div>
`;


});


function startExam(id, time) {
    const TIME_LIMIT = $('.quizz__time').children()[1].id.split("-")[1] * 60 * 60;
    let timeLeft = TIME_LIMIT;

    //animation handle
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
        $(this).css('display', 'none');
        $('#start__exam').css('display', 'none');
        $('.main-carousel').addClass("transformVisible");
        $('.submit__button').addClass("transformVisible");
        $('.clock').css('visibility', 'visible');
    });
    $('#start__exam').removeClass('bounding');

    $('#start__exam').addClass('boundingOut');

    //get data from serve
    HTTPGet('/quizz/exam', { quizzId: id }, initTesting)
        .done(function () {
            startTimer(timeLeft, TIME_LIMIT);
        })
        .fail(function (jqxhr, setting, ex) { console.log(ex) });


}

function initTesting(res) {
    console.log(1, res);

    $('.main-carousel').empty();

    let data = res.data;

    for (var i = 0; i < data.length; i++) {
        let qes = data[i];
        let lstAns = qes.Answers;
        let ans = ``;
        for (var j = 0; j < lstAns.length; j++) {
            ans += ` <label class="answer" onclick="hightLight()" id="ans-${lstAns[j].Id}-${qes.Id}">
                        ${lstAns[j].Content}
                        <input type="radio" name="ans-${qes.Id}-radio" />
                    </label>`;
        }
        let item = `<div class="carouser_ques" id="qes-${qes.Id}">
                <div class="num_ques">
                    <span>Question: ${i + 1}</span>
                </div>
                <div class="carouser_point">
                    <span>Point: ${qes.Point}</span>
                </div>
                <div class="carouser_content">
                    <span>${qes.Content}</span>
                </div>
                <div class="carouser_answer">
                        ${ans}
                </div>
            </div>`;
        $('.main-carousel').append(item);
    }
    $('.main-carousel').flickity({
        // options
        cellAlign: 'left',
        contain: true,
        adaptiveHeight: true,
        wrapAround: true
    });
}


function hightLight() {
    $('input[type="radio"]').parent().css('background-color', '#01476a');
    $('input[type="radio"]:checked').parent().css('background-color', '#00017A');
}

function submitExam(quizzId) {
    let result = [];
    let submit_result = $('.carouser_answer').children();
    submit_result.each(function (index) {
        if ($(this).children()[0].checked) {
            let lstId = $(this)[0].id.split("-");
            result.push({ ans: lstId[1], ques: lstId[2] });
        }
    });
    console.log(result);
    HTTPPost('/quizz/result', { data: result, id: quizzId }, function (res) {
        
    })
}

function onTimesUp() {
    clearInterval(timerInterval);
}

function startTimer(timeLeft, TIME_LIMIT) {
    let timePassed = 0;

    timerInterval = setInterval(() => {
        timePassed = timePassed += 1;
        timeLeft = TIME_LIMIT - timePassed;
        document.getElementById("base-timer-label").innerHTML = formatTime(
            timeLeft
        );
        setCircleDasharray(timeLeft, TIME_LIMIT);
        setRemainingPathColor(timeLeft);

        if (timeLeft === 0) {
            onTimesUp();
        }
    }, 1000);
}

function formatTime(time) {
    const minutes = Math.floor(time / 60);
    let seconds = time % 60;

    if (seconds < 10) {
        seconds = `0${seconds}`;
    }

    return `${minutes}:${seconds}`;
}

function setRemainingPathColor(timeLeft) {
    const { alert, warning, info } = COLOR_CODES;
    if (timeLeft <= alert.threshold) {
        document
            .getElementById("base-timer-path-remaining")
            .classList.remove(warning.color);
        document
            .getElementById("base-timer-path-remaining")
            .classList.add(alert.color);
    } else if (timeLeft <= warning.threshold) {
        document
            .getElementById("base-timer-path-remaining")
            .classList.remove(info.color);
        document
            .getElementById("base-timer-path-remaining")
            .classList.add(warning.color);
    }
}

function calculateTimeFraction(timeLeft, TIME_LIMIT) {
    const rawTimeFraction = timeLeft / TIME_LIMIT;
    return rawTimeFraction - (1 / TIME_LIMIT) * (1 - rawTimeFraction);
}

function setCircleDasharray(timeLeft, TIME_LIMIT) {
    const circleDasharray = `${(
        calculateTimeFraction(timeLeft, TIME_LIMIT) * FULL_DASH_ARRAY
    ).toFixed(0)} 283`;
    document
        .getElementById("base-timer-path-remaining")
        .setAttribute("stroke-dasharray", circleDasharray);
}
