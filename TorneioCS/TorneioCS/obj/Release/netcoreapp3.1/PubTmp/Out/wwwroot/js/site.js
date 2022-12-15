let qtdCompetidoresNecessarios = 16;
var btnIniciar = $("#iniciar");
var _iniciarTorneio = "./Home/IniciarTorneio"

btnIniciar.click(
    function iniciarTorneio() {
        const competidores = GetCompetidoresSelecionados();
        PostIniciarTorneio(competidores);
    });

function SelecionaCompetidor(id) {
    var cardCompetidor = $('#'+id);


    if (cardCompetidor.hasClass('selectedCard'))
    {
        qtdCompetidorNecessarios++;
        cardCompetidor.removeClass('selectedCard');

        if (qtdCompetidoresNecessarios > 0) {
            btnIniciar.addClass('d-none');
        }
        
    }
    else
    {
        qtdCompetidoresNecessarios--;

        if (qtdCompetidoresNecessarios == 0) {
            cardCompetidor.addClass('selectedCard');
            $("#textQtdCompetidoresNecessarios").text('Todos os competidores foram selecionados, é hora de batalhar!');
            btnIniciar.removeClass('d-none')
            return;
        }
        if (qtdCompetidoresNecessarios < 0) {
            qtdCompetidoresNecessarios++;
            return;
        }

        cardCompetidor.addClass('selectedCard');
    }

    $("#textQtdCompetidoresNecessarios").text('Escolha ' + qtdCompetidoresNecessarios +' competidores para iniciar o torneio.');
}

function GetCompetidoresSelecionados() {
    const competidoresSelecionados = [];
    const selectedCards = $('.selectedCard').toArray(); //aqui converti pra uma array normal, ao inves da do jquery
    selectedCards.forEach(card => competidoresSelecionados.push(card.id)); //pra cada elemento (id selecionado) ele vvai encher o array de competidores selecionados
    return competidoresSelecionados;
}

function PostIniciarTorneio(competidores) {
    return $.ajax({
        type: 'POST',
        url: _iniciarTorneio,
        data: {
            idsCompetidores: competidores //aqui passa os competidores pra controller (já é a lista <array no js> ao inves da string com varios ids separados por virgula)
        },
        success(data) { window.location = data.url },
        error: function (xhr, error, code) {
            alert(xhr);
            alert(code);
        },
    });
}