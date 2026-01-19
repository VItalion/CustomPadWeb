let landingSlideIndex = 1;
showSlides(landingSlideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(landingSlideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(landingSlideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("slide");
    let dots = document.getElementsByClassName("dot");
    if (n > slides.length) { landingSlideIndex = 1 }
    if (n < 1) { landingSlideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[landingSlideIndex - 1].style.display = "block";
    dots[landingSlideIndex - 1].className += " active";
}