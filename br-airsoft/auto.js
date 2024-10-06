// pinta o menu que esta selecionado 
var menuItem = document.querySelectorAll('.item-menu');

function abreLink()
{
    menuItem.forEach((item) => 
        item.classList.remove('ativo')
    )

    this.classList.add('ativo')
}

menuItem.forEach((item) => 
    item.addEventListener('click', abreLink)
)


//puxa o menu 

document.addEventListener('DOMContentLoaded', function() {
    var btnExp = document.querySelector('#btn-a');
    var menuSide = document.querySelector('.menu-lateral');
    
    btnExp.addEventListener('click', function() {
        menuSide.classList.toggle('expandir');
    });
});


var slide = document.querySelector("#img-carrosel"); // Onde será exibido o carrossel no HTML 
var imagens = [ // Lista com o caminho das imagens usadas
    './imagens/carrosel-1.png',
    './imagens/carrosel-4.png',
    './imagens/carrosel-8.png',
    './imagens/carrosel-3.png',
    './imagens/carrosel-2.png',
    './imagens/carrosel-5.png',
];

var time = 3500; // Tempo de exibição de cada imagem (3.5 segundos)
var fadeTime = 800; // Tempo da transição de opacidade (0.8 segundos)
var count = 0; // Variável que controla qual imagem está sendo exibida
var autoSlide; // Variável para o temporizador do carrossel

// Função para exibir a imagem com transição
function showImage() {
    slide.classList.add('fade-out');

    setTimeout(function() {
        slide.src = imagens[count];
        slide.classList.remove('fade-out');
    }, fadeTime);
}

// Função para avançar ou retroceder no carrossel
function moveSlideShow(direction) {
    if (direction === 'next') {
        count = (count < imagens.length - 1) ? count + 1 : 0; // Avança para a próxima imagem
    } else if (direction === 'prev') {
        count = (count > 0) ? count - 1 : imagens.length - 1; // Volta para a imagem anterior
    }

    showImage(); // Exibe a imagem correspondente
    resetAutoSlide(); // Reinicia o temporizador para garantir o tempo correto
}

// Função para iniciar o carrossel automático
function autoSlideShow() {
    moveSlideShow('next');
    autoSlide = setTimeout(autoSlideShow, time); // Configura o temporizador automático
}

// Função para resetar o carrossel automático quando um botão é clicado
function resetAutoSlide() {
    clearTimeout(autoSlide); // Para o temporizador atual
    autoSlide = setTimeout(autoSlideShow, time); // Reinicia o temporizador automático
}

// Eventos para os botões de avançar e voltar
document.querySelector("#btn-volta").addEventListener("click", function() {
    moveSlideShow('prev');
});

document.querySelector("#btn-avanca").addEventListener("click", function() {
    moveSlideShow('next');
});

// Inicia o carrossel ao carregar a página
window.onload = function() {
    autoSlideShow();
};

// mostra senha

function mostrarSenha(){
    const mostraSenha = document.getElementById('show_password');
    const senha = document.getElementById('password');

    if(mostraSenha.checked){
        console.log("checkbox marcado")
        senha.setAttribute('type', 'text')
    }else{
        console.log("checkbox desmarcado")
        senha.setAttribute('type', 'password')
    }
}