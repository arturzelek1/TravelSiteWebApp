let currentImageIndex = 0;
const images = ['../Assets/Thailand.jpg', '../Assets/bulgaria.jpeg', '../Assets/Croatia.jpg', '../Assets/France.jpg', '../Assets/Italy.jpg']; // Add paths to your images here

function showImage(index) {
    const imgElement = document.querySelector('.gallery-image');
    imgElement.src = images[index];
}

function nextImage() {
    currentImageIndex = (currentImageIndex + 1) % images.length;
    showImage(currentImageIndex);
}

function previousImage() {
    currentImageIndex = (currentImageIndex - 1 + images.length) % images.length;
    showImage(currentImageIndex);
}

// Initialize the gallery with the first image
showImage(currentImageIndex);