document.addEventListener('DOMContentLoaded', function () {
    var tabs = document.querySelectorAll(".tabs_wrapNew ul li");
    var info = document.querySelector(".info");
    var task = document.querySelector(".task");

    
    tabs.forEach((tab) => {
        tab.addEventListener("click", () => {
            tabs.forEach((tab) => {
                tab.classList.remove("active");
            })
            tab.classList.add("active");
            var tabval = tab.getAttribute("data-tabs");

            info.style.display = "none";
            task.style.display = "none";

            if (tabval == "info") {
                info.style.display = "block";
            }
            else if (tabval == "task") {
                task.style.display = "block";
            }
        })
    });

    
    document.querySelector("[data-tabs='info']").click();
});

const circularProgress = document.querySelectorAll(".circular-progress");

Array.from(circularProgress).forEach((progressBar) => {
    const progressValue = progressBar.querySelector(".percentage");
    const innerCircle = progressBar.querySelector(".inner-circle");
    let startValue = 0,
        endValue = Number(progressBar.getAttribute("data-percentage")),
        speed = 30,
        progressColor = progressBar.getAttribute("data-progress-color");

    const progress = setInterval(() => {
        startValue++;
        progressValue.textContent = `${startValue}%`;
        progressValue.style.color = `${progressColor}`;

        innerCircle.style.backgroundColor = `${progressBar.getAttribute(
            "data-inner-circle-color"
        )}`;

        progressBar.style.background = `conic-gradient(${progressColor} ${startValue * 3.6
            }deg,${progressBar.getAttribute("data-bg-color")} 0deg)`;
        if (startValue === endValue) {
            clearInterval(progress);
        }
    }, speed);
});