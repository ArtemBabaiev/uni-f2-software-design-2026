namespace BaseDotNetApp.Lecture14;

public class ControllerBad
{

    public class Order(string customerId)
    {
        private decimal total;
        public void SetTotal(decimal value)
        {
            this.total = value;
        }
    }
    public class InventoryRepository
    {
        
        private List<Order> orders = new List<Order>();
        public void Save(Order order)
        {
            this.orders.Add(order);
        }
    }
    
    // Антипатерн: UI містить доменну логіку та самостійно керує інфраструктурою
    public class DesktopCheckoutView
    {
        private readonly InventoryRepository _repository = new();

        public void OnCheckoutButtonClicked(string customerId, decimal totalAmount)
        {
            // Бізнес-правило прописане в шарі UI
            if (totalAmount > 5000)
            {
                Console.WriteLine("Потрібне схвалення менеджером!");
                return;
            }

            var order = new Order(customerId);
            order.SetTotal(totalAmount);
        
            _repository.Save(order);
            Console.WriteLine("Замовлення збережено.");
        }
    }
}

public class Controller
{
    public class Order(string customerId)
    {
        private decimal total;
        public void SetTotal(decimal value)
        {
            this.total = value;
        }
    }
    public class InventoryRepository
    {
        
        private List<Order> orders = new List<Order>();
        public void Save(Order order)
        {
            this.orders.Add(order);
        }
    }
    
    // Транспортна модель (Command) для передачі наміру
    public record CheckoutCommand(string CustomerId, decimal TotalAmount);

    // GRASP Controller: Application Service, що координує процес
    public class CheckoutUseCaseController(InventoryRepository repository)
    {
        private readonly InventoryRepository _repository = repository;

        public void Handle(CheckoutCommand command)
        {
            // Координація бізнес-правил (сама логіка делегується 도менним моделям,
            // але контролер управляє потоком)
            if (command.TotalAmount > 5000)
            {
                throw new InvalidOperationException("Замовлення перевищує ліміт авто-схвалення.");
            }

            var order = new Order(command.CustomerId);
            order.SetTotal(command.TotalAmount);
        
            _repository.Save(order);
        }
    }

    // UI стає тонким клієнтом (Thin Client), який лише транслює подію
    public class DesktopCheckoutView(CheckoutUseCaseController controller)
    {
        private readonly CheckoutUseCaseController _controller = controller;

        public void OnCheckoutButtonClicked(string customerId, decimal totalAmount)
        {
            try
            {
                var command = new CheckoutCommand(customerId, totalAmount);
                _controller.Handle(command);
                Console.WriteLine("Замовлення збережено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }

} 