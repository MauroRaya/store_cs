namespace Store {
    internal class Program {
        static void Main(string[] args) {
            var customer1 = new Customer("Subaru");
            var customer2 = new Customer("Emilia");

            Publisher.AddSubscriber(customer1, typeof(Smartphone));
            Publisher.AddSubscriber(customer1, typeof(Computer));
            Publisher.AddSubscriber(customer2, typeof(Smartphone));

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
            private static readonly Dictionary<Type, List<ISubscriber>> _subscribers = new();

            public static void AddSubscriber(ISubscriber subscriber, Type productType) {
                if (!_subscribers.ContainsKey(productType)) {
                    _subscribers[productType] = new List<ISubscriber>();
                }
                if (!_subscribers[productType].Contains(subscriber)) {
                    _subscribers[productType].Add(subscriber);
                }
            }

            public static void NotifySubscribers(IProduct product) {
                Type productType = product.GetType();

                if (_subscribers.TryGetValue(productType, out List<ISubscriber> subscribers)) {
                    string message = product.NewReleaseMessage();

                    foreach (var subscriber in subscribers) {
                        subscriber.Notificate(message);
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