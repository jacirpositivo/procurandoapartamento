# Projeto Procurando Apartamento

Você provê um serviço por web de busca de apartamentos para locatários. Neste serviço, neste lugar específico da cidade mostrado no mapa abaixo, estão disponíveis diversos apartamentos em cada uma das quadras. As quadras, além de conter os apartamentos, mostram onde a rua começa e onde termina já que a quadra 1 é o início da rua e quadra (n) é mais próximo a seu final. O mapa não determina, porém, o sentido de pedestres e carros já que é mão dupla. É disponibilizada para esse serviço uma tabela da relação de serviços disponíveis em cada uma das quadras: academia, escola e mercado. Os locatários são pessoas muito ocupadas e trabalham remotamente. Assim, é muito importante que eles morem em um lugar com menor deslocamento possível. O serviço deve, com base na indicação do locatário em necessidades de serviços, buscar um apartamento dentro da quadra que proporcione menor deslocamento para seu dia a dia. 

![image](https://user-images.githubusercontent.com/112395489/187196886-8818cda2-395c-4968-8831-645211fb303b.png)

A tabela mostra a relação de “ApartamentosDisponiveis” e os estabelecimentos existentes na quadra. Você deve usá-la para tomar a decisão sobre a melhor escolha de imóvel para seu cliente.

![image](https://user-images.githubusercontent.com/112395489/187196932-38fe6a55-7e7e-4a26-a463-3a3e015335dc.png)

A escolha do apartamento vai depender de:
1.	Ter um apartamento disponível na quadra;
2.	Andar o mínimo possível para encontrar um estabelecimento de seu interesse;
3.	A Prioridade de estabelecimentos depende da ordem de entrada de dados. Então se a entrada de dados for ACADEMIA e ESCOLA por exemplo, deve-se priorizar apartamentos mais próximos de ACADEMIAS e depois de ESCOLAS;
4.	O critério de desempate em caso de mais de uma quadra atender aos requisitos é escolher a quadra mais próxima ao final da rua.  

## Exemplos:

Considerando os testes abaixo, os resultados seriam os seguintes: 

- ['ACADEMIA', 'MERCADO'] 
    - O resultado deve ser QUADRA 1, pois na quadra 1 tem apartamento disponível e os dois estabelecimentos de seu interesse;

- ['ACADEMIA'] 
    - O resultado deve ser QUADRA 2, pois na quadra 2 tem apartamento disponível, tem academia e está mais próximo do final da rua;

## O que entregar:
1.	Algoritmo de resolução (Novo método no Controller Apartamento) Exemplo a seguir:
![image](https://user-images.githubusercontent.com/90634328/187257056-3d55d9e0-11c2-416e-9c07-2eba82665fcf.png)

    - A entrada de dados será um Array de String, e o return do Método será uma String. Ex: "Quadra 5";
    - Qual seria o resultado caso as entradas sejam:
        - ['ESCOLA', 'ACADEMIA'];
        - ['ESCOLA', 'MERCADO', 'ACADEMIA'];
    
2.	Criar testes unitários baseado nos exemplos passados;
![image](https://user-images.githubusercontent.com/90634328/187255894-6ff4700d-0e97-4435-af29-a615925c6d20.png)


## Instruções Gerais
- Você deverá fazer:
 - Fork desse projeto em um repositório Privado, no Seu perfil, com o Titulo "Desenvolvedor-Positivo";
    - ![image](https://user-images.githubusercontent.com/90634328/187271331-e6582814-f28b-47d8-a940-f39d76a1f62d.png)
    -![image](https://user-images.githubusercontent.com/90634328/187272263-00e338ce-eb0c-474f-ab72-6c730060df56.png)

- Resolvê-lo e subir ao seu repositório Privado
- Os acessos devem ser dados para os usuários 'jacirpositivo' e 'ctomasini' apenas;
- O README.md deverá ser o seu curriculo, com a adequada formatação.

## Requisitos de software para executar o projeto
- net6.0 https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- Visual Studio 2022 (https://visualstudio.microsoft.com/pt-br/vs/) ou Visual Studio Code (https://code.visualstudio.com/)
