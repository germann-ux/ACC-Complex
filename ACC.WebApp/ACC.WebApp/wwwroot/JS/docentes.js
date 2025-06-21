// --- FUNCIONES GLOBALES PARA BLZOR JSINTEROP ---

window.activarTab = function (targetTabId) {
    const tabButtons = document.querySelectorAll('.tab-button');
    const contentCards = document.querySelectorAll('.container > .card:not(#formularioCrearAviso)');
    const formularioAvisoCard = document.getElementById('formularioCrearAviso');

    tabButtons.forEach(btn => btn.classList.remove('active'));
    contentCards.forEach(card => card.classList.remove('active'));

    const activeTabButton = document.querySelector(`.tab-button[data-tab="${targetTabId}"]`);
    const activeContentCard = document.getElementById(targetTabId);

    if (activeTabButton) activeTabButton.classList.add('active');
    if (activeContentCard) activeContentCard.classList.add('active');
    // Si el formulario de aviso está abierto, lo ocultamos
    if (formularioAvisoCard && formularioAvisoCard.style.display === 'block') {
        window.ocultarFormularioAviso && window.ocultarFormularioAviso();
    }
};

window.copiarEnlace = function () {
    const enlace = "https://aula-acc.com/invitacion/c-sharp-intermedio";
    navigator.clipboard.writeText(enlace)
        .then(() => { alert("Enlace copiado al portapapeles: " + enlace); })
        .catch(err => { console.error('Error al copiar: ', err); });
};

window.mostrarDetallesEstudiante = function (nombre, progreso, ultimaLeccion, calificaciones) {
    const contenido = document.getElementById('studentDetailsContent');
    let calificacionesHTML = '';
    if (calificaciones && calificaciones.length > 0) {
        calificaciones.forEach(cal => {
            calificacionesHTML += `<li><i class="fas fa-clipboard-list"></i> ${cal[0]}: ${cal[1]}</li>`;
        });
    } else {
        calificacionesHTML = '<li><i class="fas fa-info-circle"></i> No hay calificaciones registradas.</li>';
    }

    let progressBarColor;
    if (progreso <= 33) {
        progressBarColor = '#dc3545'; // Rojo
    } else if (progreso <= 66) {
        progressBarColor = '#ffc107'; // Amarillo
    } else {
        progressBarColor = '#28a745'; // Verde
    }

    contenido.innerHTML = `
        <p><i class="fas fa-user"></i><strong>Nombre:</strong> ${nombre}</p>

        <div class="progress-info-container">
          <span class="progress-label"><i class="fas fa-tasks"></i><strong>Progreso:</strong></span>
          <div class="progress-bar-container">
            <div class="progress-bar" style="width: ${progreso}%; background-color: ${progressBarColor};" aria-valuenow="${progreso}" aria-valuemin="0" aria-valuemax="100">${progreso}%</div>
          </div>
        </div>

        <p><i class="fas fa-book"></i><strong>Última lección:</strong> ${ultimaLeccion}</p>

        <p><i class="fas fa-star"></i><strong>Calificaciones:</strong></p>
        <ul>${calificacionesHTML}</ul>`;

    document.getElementById('overlay').style.display = 'block';
    document.getElementById('studentDetailsPopup').style.display = 'block';
};

window.cerrarDetallesEstudiante = function () {
    document.getElementById('overlay').style.display = 'none';
    document.getElementById('studentDetailsPopup').style.display = 'none';
};

window.mostrarFormularioAviso = function () {
    const form = document.getElementById('formularioCrearAviso');
    if (form) form.style.display = 'block';
};

window.ocultarFormularioAviso = function () {
    const form = document.getElementById('formularioCrearAviso');
    if (form) {
        form.style.display = 'none';
        if (form.querySelector('form')) {
            form.querySelector('form').reset();
        }
    }
};

// --- INICIALIZACIÓN AUTOMÁTICA DE TABS AL CARGAR ---
document.addEventListener('DOMContentLoaded', function () {
    const tabButtons = document.querySelectorAll('.tab-button');
    const initialActiveTab = document.querySelector('.tab-button.active');
    if (initialActiveTab) {
        window.activarTab(initialActiveTab.getAttribute('data-tab'));
    } else if (tabButtons.length > 0) {
        window.activarTab(tabButtons[0].getAttribute('data-tab'));
    }
});