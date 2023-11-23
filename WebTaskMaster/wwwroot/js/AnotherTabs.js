document.addEventListener('DOMContentLoaded', function () {
    var tabs = document.querySelectorAll(".tabs_wrapNew ul li");
    var info = document.querySelector(".info");
    var task = document.querySelector(".task");

    // Инициализация при загрузке страницы
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

    // Сделаем активной вкладку "Project Information" после обработки кликов
    document.querySelector("[data-tabs='info']").click();
});