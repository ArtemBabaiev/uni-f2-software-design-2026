namespace BaseDotNetApp.Lecture15;

public class Prototype
{
}

// 1. Інтерфейс Prototype
public interface IGameEntityPrototype
{
    string GetFaction();
    IGameEntityPrototype Clone();
}

// 2. ConcretePrototype: Реалізація базового прототипу (Солдат)
// Використання Primary Constructor для стандартної ініціалізації
public class SoldierEntity : IGameEntityPrototype
{
    private int _health;
    private int _damage;
    private string _faction;

    public SoldierEntity(int health, int damage, string faction)
    {
        _health = health;
        _damage = damage;
        _faction = faction;
    }

    // Конструктор копіювання: інкапсулює логіку копіювання стану.
    // Захищений, щоб унеможливити пряме використання поза ієрархією.
    protected SoldierEntity(SoldierEntity prototype)
    {
        _health = prototype._health;
        _damage = prototype._damage;
        _faction = prototype._faction;
        // Саме тут реалізується логіка глибокого копіювання вкладених об'єктів (якби вони були)
    }

    public string GetFaction() => _faction;

    // Реалізація методу clone() через повернення нового екземпляра з передачею 'this'
    public virtual IGameEntityPrototype Clone()
    {
        return new SoldierEntity(this);
    }

    public override string ToString() => $"Soldier [Faction={_faction}, HP={_health}, Dmg={_damage}]";
}

// 3. SubclassPrototype: Розширений прототип (Елітний солдат)
public class EliteSoldierEntity : SoldierEntity
{
    private string _specialAbility;

    public EliteSoldierEntity(int health, int damage, string faction, string specialAbility)
        : base(health, damage, faction)
    {
        _specialAbility = specialAbility;
    }

    // Конструктор копіювання похідного класу. 
    // Спочатку викликає base(prototype), щоб скопіювати базовий стан.
    protected EliteSoldierEntity(EliteSoldierEntity prototype) : base(prototype)
    {
        _specialAbility = prototype._specialAbility;
    }

    public override IGameEntityPrototype Clone()
    {
        return new EliteSoldierEntity(this);
    }

    public override string ToString() => base.ToString() + $" [Ability={_specialAbility}]";
}

// 4. Prototype Registry: Централізоване сховище еталонів
public class EntityRegistry
{
    // Колекція для збереження прототипів.
    private readonly Dictionary<string, IGameEntityPrototype> _items = new();

    public void AddItem(string id, IGameEntityPrototype prototype)
    {
        _items[id] = prototype;
    }

    public IGameEntityPrototype GetById(string id)
    {
        if (_items.TryGetValue(id, out var prototype))
        {
            // КРИТИЧНО: Повертаємо саме клон, а не оригінал
            return prototype.Clone();
        }

        throw new KeyNotFoundException($"Прототип з ID '{id}' не знайдено.");
    }

    // Пошук за критерієм фракції (аналог getByColor з діаграми)
    public IGameEntityPrototype? GetByFaction(string faction)
    {
        foreach (var item in _items.Values)
        {
            if (item.GetFaction().Equals(faction, StringComparison.OrdinalIgnoreCase))
            {
                return item.Clone();
            }
        }

        return null;
    }
}

// 5. Клієнтський код
public class GameEngineClient
{
    public void RunSimulation()
    {
        var registry = new EntityRegistry();

        // Реєстрація еталонів на старті системи
        registry.AddItem("StandardOrc", new SoldierEntity(100, 15, "Orcs"));
        registry.AddItem("EliteElf", new EliteSoldierEntity(150, 30, "Elves", "Invisibility"));

        // Масова генерація через клонування без виклику складних конструкторів
        if (registry.GetByFaction("Elves") is EliteSoldierEntity clonedElf)
        {
            Console.WriteLine($"Створено через критерій: {clonedElf}");
        }
    }
}