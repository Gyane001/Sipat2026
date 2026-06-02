-> SituacaoADM_1_1

=== SituacaoADM_1_1 ===
#portrait:default
Há uma placa de “Piso Molhado” e uma pequena área úmida no chão. Qual seu comportamento?

*[Passo rapidamente antes que alguém veja]
    Ignorar a sinalização aumenta o risco de escorregões, quedas e lesões. Sempre desvie da área sinalizada (molhada) evitando acidentes. #portrait:PF_ADM_11
    -> Fim

*[Ignoro a placa e passo pela área molhada]
    Ignorar a sinalização aumenta o risco de escorregões, quedas e lesões. Sempre desvie da área sinalizada (molhada) evitando acidentes. #portrait:PF_ADM_13
    -> Fim
    
    *[Desvio da área sinalizada]
    Muito bem! Pisos molhados podem ser grandes causadores de quedas em áreas administrativas. Prevenção evita acidentes.#portrait:PF_ADM_12 #win 
    -> Fim

=== Fim ===
Siga em frente para continuar. #portrait:default
-> DONE