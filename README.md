# Test-Actors
 
тест акторсов на классах-компонентах. 

- настройки дефолтные для IL2CPP только включена галка на GC experimental. 
- строчка scripting define ACTORS_TAGS_6; (все эти настройки в tools->Actors меню эдитора можно отключить\включить)

1) ввести число обьектов, которые будут перемещаться по экрану, нажать "ввод"
2) нажать кнопку нужного теста

..
- создаются объекты в скриптах с соответствующим названием ModelChief[---];
- перемещаются объекты в ProcessorMove[---]
- структурка obj просто как "записная книжка" для коллайдера, который зависит только от позиции обьекта и размера float2(10f, 10f). 
поворот коллайдера не учитывается, но считается просто так для нагрузки.
