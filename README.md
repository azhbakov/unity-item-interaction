# unity-item-interaction

В этом проекте я пробовал воспроизвести систему управления и взаимодействия из игры SpaceStation 13.

К сожалению, файлы проекта Unity потеряны, остался лишь исходный код :(

У игрока две "руки" - два слота для взаимодействия с предметами. Рукой можно поднять предмет (ЛКМ) или взаимодействовать с предметом через контекстное меню (ПКМ). Активная рука выбирается клавишами Q (левая) и E (правая). Если рука занята предметом, то ей нельзя взаимодействовать с другими предметами. Предмет может быть адресован как через объект игрового мира, так и через пользовательский интерфейс, если, например, предмет не инстанцирован в сцене и существует лишь как свойство удерживающего его персонажа.

В тестовой сцене в качестве предметов лежат два красных бокса, и большой белый бокс как транспортное средство (можно в него "сесть" клавишей F).

Используется довольно сложная иерархия классов для обеспечения высокого уровня абстракции и возможности использовать игровые предметы с самыми разными назначениями (например, одним предметом можно воздействовать на другой, например, предметом на рюкзак - положить предмет, отверткой на механизм - много возможных действий, и т.д.)

Есть заготовка с графическим интерфейсом (скриншот доступен https://github.com/azhbakov/unity-item-interaction/wiki). Для каждого предмета предусмотрено отображение в качестве иконки в интерфейсе и полноценная модель в игровом пространстве.
