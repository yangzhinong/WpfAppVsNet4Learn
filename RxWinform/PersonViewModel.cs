using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RxWinform
{
    public class PersonViewModel : ReactiveObject
    {
        [Reactive]
        public int Id { get; set; }
        [Reactive]
        public string Name { get; set; }
        [Reactive]
        public int Age { get; set; }

        public PersonViewModel()
        {
            Id = 1;
            Name = "张三";
            Age = 18;
        }
    }
}
