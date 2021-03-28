

function hideOnClick(ele, id) {
    let s = '#'+id + 'c';
    if ($(s).hasClass("toLeftTransition")||$(s).hasClass("hide")) {
        return;
    }
    let code = id;
    let leftPos = ele.offsetLeft - 50;
    let topPos = ele.offsetTop - 50;
    ele.style.left = leftPos + 'px';
    ele.style.top = topPos + 'px';
    ele.style.position = 'absolute';
    $(".list__item").toggleClass("hide");
    $(".list__item").removeClass("up");

    ele.classList.remove("hide");
    setTimeout(function () {
        ele.classList.add("toLeftTransition");
    }, 1200);

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
    let list=``;
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



