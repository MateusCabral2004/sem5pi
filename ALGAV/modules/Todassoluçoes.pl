
:- dynamic availability/3.%:- retractall(availability(_,_,_)).
:-dynamic agenda_operation_room/3.
:-dynamic agenda_staff/3.
:- dynamic agenda_staff1/3.
:-dynamic agenda_operation_room1/3.
:-dynamic assignment_surgery/2.



%another example
agenda_staff(d001,20241028,[(720,790,m01),(1080,1140,c01)]).
agenda_staff(d002,20241028,[(850,900,m02),(901,960,m02),(1380,1440,c02)]).
agenda_staff(d003,20241028,[(720,790,m01),(910,980,m02)]).

timetable(d001,20241028,(480,1200)).
timetable(d002,20241028,(720,1440)).
timetable(d003,20241028,(600,1320)).

%another example
%timetable(d001,20241028,(480,1200)).
%timetable(d002,20241028,(500,1440)).
%timetable(d003,20241028,(520,1320)).


staff(d001,doctor,orthopaedist,[so2,so3,so4]).
staff(d002,doctor,orthopaedist,[so2,so3,so4]).
staff(d003, doctor, anesthetist, [so2, so3]).
staff(d004, doctor, orthopaedist, [so2]).


surgery_duration(so3, 10). % Exemplo: cirurgia so3 dura 120 minutos.
surgery_duration(so2, 90).  % Exemplo: cirurgia so2 dura 90 minutos.
surgery_duration(so4, 90).  % Exemplo: cirurgia so2 dura 90 minutos.


% Requisitos de staff para cirurgias

surgery_staff_requirements(so3, [
    (orthopaedist, 2),
    (anesthetist, 1)
]).

surgery_staff_requirements(so4, [
    (orthopaedist, 2),
    (anesthetist, 1)
]).
surgery_staff_requirements(so2, [
    (orthopaedist, 2),
    (anesthetist, 1)
]).
agenda_operation_room(or1,20241028,[(520,579,so100000),(1000,1059,so099999)]).
agenda_operation_room(ola,20241028,[]).


surgery(so2,45,60,45).
surgery(so3,45,90,45).
surgery(so4,45,75,45).

surgery_id(so100001,so2).
surgery_id(so100002,so3).
surgery_id(so100003,so4).
surgery_id(so100004,so2).
surgery_id(so100005,so4).

assignment_surgery(so100001,d001).






find_free_agendas(Date):-retractall(availability(_,_,_)),findall(_,(agenda_staff(D,Date,L),free_agenda_staff0(L,LFA),adapt_timetable(D,Date,LFA,LFA2),assertz(availability(D,Date,LFA2))),_).


free_agenda_staff0([],[(0,1440)]).
free_agenda_staff0([(0,Tfin,_)|LT],LT1):-!,free_agenda_staff1([(0,Tfin,_)|LT],LT1).
free_agenda_staff0([(Tin,Tfin,_)|LT],[(0,T1)|LT1]):- T1 is Tin-1,
    free_agenda_staff1([(Tin,Tfin,_)|LT],LT1).

free_agenda_staff1([(_,Tfin,_)],[(T1,1440)]):-Tfin\==1440,!,T1 is Tfin+1.
free_agenda_staff1([(_,_,_)],[]).
free_agenda_staff1([(_,T,_),(T1,Tfin2,_)|LT],LT1):-Tx is T+1,T1==Tx,!,
    free_agenda_staff1([(T1,Tfin2,_)|LT],LT1).
free_agenda_staff1([(_,Tfin1,_),(Tin2,Tfin2,_)|LT],[(T1,T2)|LT1]):-T1 is Tfin1+1,T2 is Tin2-1,
    free_agenda_staff1([(Tin2,Tfin2,_)|LT],LT1).


adapt_timetable(D,Date,LFA,LFA2):-timetable(D,Date,(InTime,FinTime)),treatin(InTime,LFA,LFA1),treatfin(FinTime,LFA1,LFA2).

treatin(InTime,[(In,Fin)|LFA],[(In,Fin)|LFA]):-InTime=<In,!.
treatin(InTime,[(_,Fin)|LFA],LFA1):-InTime>Fin,!,treatin(InTime,LFA,LFA1).
treatin(InTime,[(_,Fin)|LFA],[(InTime,Fin)|LFA]).
treatin(_,[],[]).

treatfin(FinTime,[(In,Fin)|LFA],[(In,Fin)|LFA1]):-FinTime>=Fin,!,treatfin(FinTime,LFA,LFA1).
treatfin(FinTime,[(In,_)|_],[]):-FinTime=<In,!.
treatfin(FinTime,[(In,_)|_],[(In,FinTime)]).
treatfin(_,[],[]).


intersect_all_agendas([Name],Date,LA):-!,availability(Name,Date,LA).
intersect_all_agendas([Name|LNames],Date,LI):-
    availability(Name,Date,LA),
    intersect_all_agendas(LNames,Date,LI1),
    intersect_2_agendas(LA,LI1,LI).

intersect_2_agendas([],_,[]).
intersect_2_agendas([D|LD],LA,LIT):-	intersect_availability(D,LA,LI,LA1),
					intersect_2_agendas(LD,LA1,LID),
					append(LI,LID,LIT).

intersect_availability((_,_),[],[],[]).

intersect_availability((_,Fim),[(Ini1,Fim1)|LD],[],[(Ini1,Fim1)|LD]):-
		Fim<Ini1,!.

intersect_availability((Ini,Fim),[(_,Fim1)|LD],LI,LA):-
		Ini>Fim1,!,
		intersect_availability((Ini,Fim),LD,LI,LA).

intersect_availability((Ini,Fim),[(Ini1,Fim1)|LD],[(Imax,Fmin)],[(Fim,Fim1)|LD]):-
		Fim1>Fim,!,
		min_max(Ini,Ini1,_,Imax),
		min_max(Fim,Fim1,Fmin,_).

intersect_availability((Ini,Fim),[(Ini1,Fim1)|LD],[(Imax,Fmin)|LI],LA):-
		Fim>=Fim1,!,
		min_max(Ini,Ini1,_,Imax),
		min_max(Fim,Fim1,Fmin,_),
		intersect_availability((Fim1,Fim),LD,LI,LA).


min_max(I,I1,I,I1):- I<I1,!.
min_max(I,I1,I1,I).




    % Predicado para listar e agendar cirurgias pendentes
    search_pending_surgeries(Date, Room) :-
    findall(Surgery, surgery_id(Surgery, _), SurgeryList),
    schedule_pending_surgeries(SurgeryList,Date,Room).
     
% Caso base: quando a lista de cirurgias está vazia.
schedule_pending_surgeries([], _, _) :-
    format('Todas as cirurgias pendentes foram processadas.\n', []).

% Caso recursivo: processa a cirurgia atual e continua com o restante.
schedule_pending_surgeries([X | Rest], Date, Room) :-
    (   
        schedule_surgery(X, Date, Room) ->
        format('Cirurgia ~w agendada com sucesso na sala ~w na data ~w.\n\n\n', [X, Room, Date])
    ;  
        format('Falha ao agendar a cirurgia ~w na sala ~w na data ~w.\n\n', [X, Room, Date])
    ),
    schedule_pending_surgeries(Rest, Date, Room).


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
schedule_surgery(Surgery, Date, Room) :-
    % Obter o tipo de cirurgia
    surgery_id(Surgery, Tipo),
    format('Iniciando agendamento para a cirurgia ~w...\n', [Surgery]),

    % Obter os requisitos de staff para a cirurgia
    surgery_staff_requirements(Tipo, Requirements),
    format('Requisitos de staff para a cirurgia ~w: ~w\n', [Surgery, Requirements]),

    % Obter a duração da cirurgia
    surgery_duration(Tipo, Duration),
    format('Duração estimada para a cirurgia ~w: ~w minutos\n', [Surgery, Duration]),

    % Encontrar agendas livres
    find_free_agendas(Date),

    % Obter a lista de staff para a cirurgia
    get_staff_for_surgery(Requirements, StaffList),
    format('Equipes disponíveis para a cirurgia ~w: ~w\n', [Surgery, StaffList]),

    % Encontrar intervalos comuns entre as equipes
    findall(CommonIntervals, (member(Team, StaffList), intersect_all_agendas(Team, Date, CommonIntervals)), AllCommonIntervals),
    format('Intervalos comuns encontrados: ~w\n', [AllCommonIntervals]),

    % Verificar se há intervalos suficientes para realizar a cirurgia
    findall(SurgeryInterval, (member(CommonIntervals, AllCommonIntervals), select_sufficient_interval(CommonIntervals, Duration, SurgeryInterval)), AllSurgeryInterval),
    format('Intervalos suficientes encontrados: ~w\n', [AllSurgeryInterval]),

    % Verificar disponibilidade da sala
    findall(SurgeryInterval, (member(SurgeryInterval, AllSurgeryInterval), check_room_availability(Room, Date, SurgeryInterval)), ValidRoomIntervals),
    format('Intervalos válidos com a sala disponível: ~w\n', [ValidRoomIntervals]),

    % Obter o intervalo mais tardio
    min_final_minute(AllSurgeryInterval, MinInterval),
    format('Menor hora final selecionado: ~w\n', [MinInterval]),

    % Encontrar as equipes com o intervalo mais tardio
    find_staff_with_min_interval(StaffList, Date, MinInterval, StaffWithMinInterval),
    format('Equipes com o intervalo ~w disponível: ~w\n', [MinInterval, StaffWithMinInterval]),

    % Se houver equipes disponíveis, selecionar uma e atualizar as agendas
    
         [SelectedTeam | _] = StaffWithMinInterval,  % Seleciona a primeira equipe disponível
         format('Equipe selecionada: ~w\n', [SelectedTeam]),
 
 
        % Adicionar a atribuição da cirurgia
        add_assignment_surgery(Surgery, Room),
        format('Cirurgia ~w atribuída à sala ~w.\n', [Surgery, Room]),
 
         % Atualizar a agenda da equipe
         update_staff_agendas(SelectedTeam, Date, MinInterval, Surgery),   
         format('Agenda da equipe ~w atualizada para a cirurgia ~w.\n', [SelectedTeam, Surgery]),
 
         % Atualizar a agenda da sala
         update_room_agenda(Room, Date, MinInterval, Surgery),
         format('Agenda da sala ~w atualizada para a cirurgia ~w.\n', [Room, Surgery]).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

get_staff_for_surgery([], [[]]). % Nenhum requisito restante resulta em uma lista vazia como única possibilidade.
get_staff_for_surgery([(Type, Quantity) | RestRequirements], AllStaffLists) :-
    findall(ID, staff(ID, _, Type, _), AllStaff), % Encontra todos os IDs de staff com o tipo requerido.
    findall(Selected, (combination(Quantity, AllStaff, Selected)), PossibleSelections), % Todas as combinações de tamanho Quantity.
    get_staff_for_surgery(RestRequirements, RestStaffLists), % Recursivamente para o restante dos requisitos.
    findall(AppendedList,
            (member(Selected, PossibleSelections), % Para cada seleção possível do requisito atual...
             member(RestStaff, RestStaffLists),   % ...combine com as possibilidades do resto.
             append(Selected, RestStaff, AppendedList)), 
            AllStaffLists). % Produz todas as combinações possíveis.

combination(0, _, []). 
combination(N, [H|T], [H|R]) :- 
    N > 0, 
    N1 is N - 1, 
    combination(N1, T, R).
combination(N, [_|T], R) :- 
    N > 0, 
    combination(N, T, R).


select_sufficient_interval([(Start, End) | Rest], Duration, (Start, End)) :-
    End - Start + 1 >= Duration,
    (Rest = [] ; 
     \+ (member((Start2, End2), Rest), 
         End2 - Start2 + 1 >= Duration, 
         Start2 < Start))
    .
select_sufficient_interval([_ | Rest], Duration, Interval) :-
    select_sufficient_interval(Rest, Duration, Interval).

check_room_availability(Room, Date, (Start, End)) :-
    agenda_operation_room(Room, Date, RoomAgenda),
     overlaps_with_existing(RoomAgenda, (Start, End)).



overlaps_with_existing([(AStart, AEnd, _) | _], (Start, End)) :-
    Start =< AEnd, End >= AStart, !.
overlaps_with_existing([_ | Rest], Interval) :-
    overlaps_with_existing(Rest, Interval).
overlaps_with_existing([], _).

update_staff_agendas([], _, _, _).
update_staff_agendas([ID | Rest], Date, (Start, End), Surgery) :-
    agenda_staff(ID, Date, CurrentAgenda),

    append(CurrentAgenda, [(Start, End, Surgery)], UpdatedAgenda),

    retractall(agenda_staff(ID, Date, _)),  
    assertz(agenda_staff(ID, Date, UpdatedAgenda)),  

    update_staff_agendas(Rest, Date, (Start, End), Surgery).


%Melhorar
%Surgery- tem de ser o id da cirrugia, ou seja tenho de cria uma nova cirurgia
update_room_agenda(Room, Date, (Start, End), Surgery) :-
    agenda_operation_room(Room, Date, CurrentAgenda),

    append(CurrentAgenda, [(Start, End, Surgery)], UpdatedAgenda),

    retractall(agenda_operation_room(Room, Date, _)),
    assertz(agenda_operation_room(Room, Date, UpdatedAgenda)).

add_assignment_surgery(SurgeryID, Room) :-
    \+assignment_surgery(SurgeryID,_),
    assertz(assignment_surgery(SurgeryID, Room)).
   

% Encontra o intervalo com o menor minuto final em AllCommonIntervals
min_final_minute(AllCommonIntervals, MinInterval) :-
    flatten(AllCommonIntervals, Flattened),
    findall((Start, End), member((Start, End), Flattened), Intervals), 
    findall(End, member((_, End), Intervals), EndMinutes), 
    min_list(EndMinutes, MinMinute), % menor minuto final
    member(MinInterval, Intervals), 
    MinInterval = (_, MinMinute). % Encontra o intervalo com o minuto final igual a MinMinute

% Encontra a equipe que tem o intervalo comum igual ao min_interval
find_staff_with_min_interval(StaffList, Date, MinInterval, StaffWithMinInterval) :-
    findall(Team, (
        member(Team, StaffList),
        intersect_all_agendas(Team, Date, CommonIntervals),
        member(MinInterval, CommonIntervals)  % Verifica se MinInterval está na lista de CommonIntervals da equipe
    ), StaffWithMinInterval).
