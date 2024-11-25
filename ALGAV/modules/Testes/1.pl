
:- dynamic availability/3.%:- retractall(availability(_,_,_)).
:-dynamic agenda_operation_room/3.
:-dynamic agenda_staff/3.



agenda_staff(d001,20241028,[(720,840,m01)]).
agenda_staff(d002,20241028,[(780,900,m02)]).
agenda_staff(d003,20241028,[(720,840,m01)]).

%another example
%agenda_staff(d001,20241028,[(720,790,m01),(1080,1140,c01)]).
%agenda_staff(d002,20241028,[(850,900,m02),(901,960,m02),(1380,1440,c02)]).
%agenda_staff(d003,20241028,[(720,790,m01),(910,980,m02)]).

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



surgery_duration(so3, 10). % Exemplo: cirurgia so3 dura 120 minutos.
surgery_duration(so2, 90).  % Exemplo: cirurgia so2 dura 90 minutos.

% Requisitos de staff para cirurgias

surgery_staff_requirements(so3, [
    (orthopaedist, 2),
    (anesthetist, 1)
]).
agenda_operation_room(or1,20241028,[(520,579,so100000),(1000,1059,so099999)]).


schedule_surgery(Surgery, Date, Room) :-
    surgery_staff_requirements(Surgery, Requirements),
    surgery_duration(Surgery, Duration),

    % tirar?
    find_free_agendas(Date),

    get_staff_for_surgery(Requirements, StaffList),

    intersect_all_agendas(StaffList, Date, CommonIntervals),

    % Verificar se a disponibilidade é suficiente para realizar a cirurgia.
    select_sufficient_interval(CommonIntervals, Duration, SurgeryInterval),

    check_room_availability(Room, Date, SurgeryInterval),

    update_staff_agendas(StaffList, Date, SurgeryInterval, Surgery),
    update_room_agenda(Room, Date, SurgeryInterval, Surgery).

get_staff_for_surgery([], []).
get_staff_for_surgery([(Type, Quantity) | RestRequirements], StaffList) :-
   findall(ID, staff(ID, _, Type, _), AllStaff),
    length(Selected, Quantity),%tamanho da lista=Quantity
    prefix(Selected, AllStaff),
    get_staff_for_surgery(RestRequirements, RestStaff),
    append(Selected, RestStaff, StaffList).


select_sufficient_interval([(Start, End) | Rest], Duration, (Start, End)) :-
    End - Start + 1 >= Duration, !.
select_sufficient_interval([_ | Rest], Duration, Interval) :-
    select_sufficient_interval(Rest, Duration, Interval).


check_room_availability(Room, Date, (Start, End)) :-
    agenda_operation_room(Room, Date, RoomAgenda),
     overlaps_with_existing(RoomAgenda, (Start, End)).



overlaps_with_existing([(AStart, AEnd, _) | _], (Start, End)) :-
    % Verifica sobreposição de intervalos.
    Start =< AEnd, End >= AStart, !.
overlaps_with_existing([_ | Rest], Interval) :-
    overlaps_with_existing(Rest, Interval).
overlaps_with_existing([], _).

update_staff_agendas([], _, _, _).
update_staff_agendas([ID | Rest], Date, Interval, Surgery) :-
    agenda_staff(ID, Date, CurrentAgenda),

    append(CurrentAgenda, [Interval-Surgery], UpdatedAgenda),

    retractall(agenda_staff(ID, Date, _)),
    assertz(agenda_staff(ID, Date, UpdatedAgenda)),

    update_staff_agendas(Rest, Date, Interval, Surgery).

update_room_agenda(Room, Date, (Start, End), Surgery) :-
    agenda_operation_room(Room, Date, CurrentAgenda),

    append(CurrentAgenda, [(Start, End, Surgery)], UpdatedAgenda),

    retractall(agenda_operation_room(Room, Date, _)),
    assertz(agenda_operation_room(Room, Date, UpdatedAgenda)).
