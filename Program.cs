namespace Store {
    internal class Program {
        static void Main(string[] args) {
            var customer1 = new Customer("Subaru");
            var customer2 = new Customer("Emilia");

            Publisher.AddSubscriber(customer1, new Smartphone());
            Publisher.AddSubscriber(customer1, new Computer());
            Publisher.AddSubscriber(customer2, new Smartphone());

            Publisher.NotifySubscribers(new Smartphone());
            Publisher.NotifySubscribers(new Computer());
        }

        interface IProduct {
            string NewReleaseMessage();
        }

        class Smartphone : IProduct {
            public string NewReleaseMessage() => "A new smartphone just released";
        }

        class Computer : IProduct {
            public string NewReleaseMessage() => "A new computer just released";
        }

        static class Publisher {
            private static Dictionary<IProduct, List<ISubscriber>> _subscribers = new();

            public static void AddSubscriber(ISubscriber subscriber, IProduct product) {
                if (_subscribers.ContainsKey(product)) {
                    throw new Exception();
                }
                _subscribers[product] = new List<ISubscriber> { subscriber };
            }

            public static void NotifySubscribers(IProduct product) {
                string message;
                foreach (var subscriber in _subscribers) {
                    if (subscriber.Key.GetType() == product.GetType()) {
                        message = product.NewReleaseMessage();
                        subscriber.Value.ForEach(func => func.Notificate(message));
                    }
                }
            }
        }

        interface ISubscriber {
            string Name { get; set; }
            void Notificate(string message);
        }

        class Customer : ISubscriber {
            public string Name { get; set; }
            public Customer(string name) {
                Name = name;
            }
            public void Notificate(string message) => Console.WriteLine($"{Name}: " + message);
        }
    }
}