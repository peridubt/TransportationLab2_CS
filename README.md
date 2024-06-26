# Лаба 2 C# (№10)

Оставлю на будущее.
Ещё вот материалы, которые помогли мне при написании лабы: _https://telegra.ph/Razlichnye-materialy-po-patternam-proektirovaniya-na-NET-05-01_ . Я не всё читал там, но ссылок сполна хватит.
###### (люди, которые берут с младших курсов деньги за уже сделанную абы как работу, у меня к вам уважения ноль)

## Задание 2
Цель — создание модели, описывающей процесс, построение иерархии классов,
отражающей данную предметную область.

**(10)** Грузоперевозки. Основные сущности: автомашины, города,
между которыми осуществляются перевозки, сеть дорог, грузы,
их типы, клиенты. Смоделировать процесс перевозки грузов.

## Требования к реализации
* Приложение реализовать как MVC. Модель — иерархия классов.
View — демо в автоматическом режиме.
* Действующие объекты должны быть реализованы как 
потоки с необходимой синхронизацией.
* Для оповещения об изменениях использовать 
шаблон «Наблюдатель».
* Для создания действующих объектов 
использовать шаблон «Фабрика».
* Использование других шаблонов приветствуется.

## Логика моей программы:
1. Автоматичекски добавляются (рандомные) грузовики: максимально — 5 шт.
2. Автоматически добавляются (рандомные) клиенты : максимально — 10 шт.
3. В форме есть конпка "Старт", которая запускает процесс выдачи заказов 
незанятым грузовикам (происходит каждые 5 секунд). Один грузовик может иметь один заказ.
Кнопка "Стоп" приостанавливает процесс выдачи заказов, но сами заказы не отменяет.
5. У каждого грузовика есть клиент, которому надо доставить груз.
Сам клиент он подписывается на event, который
в будущем уведомит его о доставке и запустит (через Invoke) процесс передачи заказа. 
После доставки клиент отписывается от данного event.
6. Один поток — это один грузовик. Сам поток запускает бесконечный цикл, 
где грузовик запрашивает каждые 100 мс, не поступил ли ему новый заказ.
При положительном результате он сразу начинает своё движение.
7. Из _MVC_ у меня:
   * Model — класс Manager + классы различных сущностей;
   * Controller — класс Animation;
   * View — классы различных винформ.
8. Из _фабрики_: фабрика грузов различных типов (я решил сделать 4 груза).
9. _Наблюдатель_: просто события, которые уведомляют клиентов.
  

    
