// adicionei o evento de ao clicar o botao executar essa funcao
const submit = document.getElementById("submitPegarNomeSetor").addEventListener("click",pegarNomeSetor);


function pegarNomeSetor(){
    const nome = document.getElementById("nomeSetor").value;
    const qtdFuncionarios = document.getElementById("qtdFuncionarios").value;
    const localizacao = document.getElementById("localizacao").value;
   
    alert(nome+"\n"+qtdFuncionarios+"\n"+localizacao);
    
    novoSetor = new SetorRequest(nome,parseInt(qtdFuncionarios, 10),localizacao);
    setor=JSON.stringify(novoSetor);
    PostSalvarSetor(setor);
   
    // addNaTabela(nomeSetor);
}


async function  PostSalvarSetor(setor){

    try{

        // passo 1 -> realizar o acesso e envio do json para a url
        const response = await fetch("http://localhost:5001/api/setores/salvarSetor",{
            method : "POST", // qual o verbo
            headers:{
                "Content-Type": "application/json" // especifica que é json
            },
            body : setor // o que será enviado em forma de json
        });

        if(!response.ok){
            throw new Error("Erro ao salvar novo setor: "+ (await response.status));
        }

        const result = await response.json(); // converte a resposta de json para objeto js
        console.log(result);
        return result;

    }catch(e){
        alert("Erro");
        console.log(e)
    }
}


class SetorRequest {
    constructor(nomeSetor,qtdFuncionarios,localizacao) { 
      this.nome = nomeSetor;  
      this.qtd_funcionarios = qtdFuncionarios;  
      this.localizacao = localizacao;  
    }
  }


function addNaTabela(nomeSetor){
    // (Criacao do node)
    // crio a tag para salvar e configuro seu valor e tipo
    const novaLinha = document.createElement("tr");
    const novoConteudo = document.createElement("td");
    novoConteudo.textContent=nomeSetor;
    novaLinha.appendChild(novoConteudo);

    //pego o id aonde quero salvar e adiciono como filho esse node/nó criado
    const tabela = document.getElementById("mostrarNomeSetor");
    tabela.appendChild(novaLinha);

}