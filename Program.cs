namespace Store {
    internal class Program {
        static void Main(string[] args) {
            Customer customer1 = new Customer();
            Customer customer2 = new Customer();
            Customer customer3 = new Customer();

            customer1.Subscribe(new Smartphone());
            customer2.Subscribe(new Smartphone());
            customer3.Subscribe(new Computer());

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
            private static Dictionary<IProduct, ISubscriber> _subscribers = new();

            public static void AddSubscriber(ISubscriber subscriber, IProduct product) {
                if (_subscribers.ContainsKey(product)) {
                    throw new Exception();
                }
                _subscribers[product] = subscriber;
            }

            public static void NotifySubscribers(IProduct product) {
                foreach (var subscriber in _subscribers) {

                    if (subscriber.Key.GetType() == product.GetType()) {
                        subscriber.Value.Notificate(product.NewReleaseMessage());
                    }
                }
            }
        }

        interface ISubscriber {
            void Subscribe(IProduct product);
            void Notificate(string message);
        }

        class Customer : ISubscriber {
            public void Subscribe(IProduct product) => Publisher.AddSubscriber(this, product);
            public void Notificate(string message) => Console.WriteLine(message);
        }
    }
}