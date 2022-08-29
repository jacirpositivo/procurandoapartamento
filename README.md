# procurandoapartamento
Projeto Procurando Apartamento
Você provê um serviço por web de busca de apartamentos para locatários. Neste serviço, neste lugar específico da cidade mostrado no mapa abaixo, estão disponíveis diversos apartamentos em cada uma das quadras. As quadras, além de conter os apartamentos, mostram onde a rua começa e onde termina já que a quadra 1 é o início da rua e quadra (n) é mais próximo a seu final. O mapa não determina, porém, o sentido de pedestres e carros já que é mão dupla. É disponibilizada para esse serviço uma tabela da relação de serviços disponíveis em cada uma das quadras: academia, escola e mercado. Os locatários são pessoas muito ocupadas e trabalham remotamente. Assim, é muito importante que eles morem em um lugar com menor deslocamento possível. O serviço deve, com base na indicação do locatário em necessidades de serviços, buscar um apartamento dentro da quadra que proporcione menor deslocamento para seu dia a dia. 

![image](https://user-images.githubusercontent.com/112395489/187196886-8818cda2-395c-4968-8831-645211fb303b.png)

A tabela mostra a relação de “ApartamentosDisponiveis” e os estabelecimentos existentes na quadra. Você deve usá-la para tomar a decisão sobre a melhor escolha de imóvel para seu cliente.

![image](https://user-images.githubusercontent.com/112395489/187196932-38fe6a55-7e7e-4a26-a463-3a3e015335dc.png)

A escolha do apartamento vai depender de:
a)	Ter um apartamento disponível na quadra;
b)	Andar o mínimo possível para encontrar um estabelecimento de seu interesse;
c)	A Prioridade de estabelecimentos depende da ordem de entrada de dados. Então se a entrada de dados for ACADEMIA e ESCOLA por exemplo, deve-se priorizar apartamentos mais próximos de ACADEMIAS e depois de ESCOLAS;
d)	O critério de desempate em caso de mais de uma quadra atender aos requisitos é escolher a quadra mais próxima ao final da rua.  

Exemplos:

Considerando os testes abaixo, os resultados seriam os seguintes: ['ACADEMIA', 'MERCADO'] 
O resultado deve ser QUADRA 1, pois na quadra 1 tem apartamento disponível e os dois estabelecimentos de seu interesse;

Considerando os testes abaixo, os resultados seriam os seguintes: ['ACADEMIA'] 
O resultado deve ser QUADRA 2, pois na quadra 2 tem apartamento disponível, tem academia e está mais próximo do final da rua;

O que entregar:
1.	Algoritmo de resolução:
    Qual seria o resultado caso as entradas sejam
    ['ESCOLA', 'ACADEMIA']
    ['ESCOLA', 'MERCADO', 'ACADEMIA']
    
2.	Definição dos testes unitários que serão utilizados;
