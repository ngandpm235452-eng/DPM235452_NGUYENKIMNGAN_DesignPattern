using System;
using System.Collections;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_IteratorPattern
{
    // Abstract Iterator
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    // Abstract Aggregate
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    // Concrete Aggregate
    public class ConcreteAggregate : Aggregate
    {
        private readonly List<object> _items = new List<object>();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count => _items.Count;

        public object this[int index]
        {
            get => _items[index];
            set => _items.Insert(index, value);
        }
    }

    // Concrete Iterator
    public class ConcreteIterator : Iterator
    {
        private readonly ConcreteAggregate _aggregate;
        private int _current = 0;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public override object First()
        {
            _current = 0;
            return _aggregate.Count > 0 ? _aggregate[0] : null;
        }

        public override object Next()
        {
            object ret = null;
            if (_current < _aggregate.Count - 1)
            {
                _current++;
                ret = _aggregate[_current];
            }
            return ret;
        }

        public override object CurrentItem()
        {
            return _aggregate[_current];
        }

        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 3: ITERATOR PATTERN (LÝ THUYẾT) ===");

            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "Phần tử 1";
            a[1] = "Phần tử 2";
            a[2] = "Phần tử 3";
            a[3] = "Phần tử 4";

            Iterator i = a.CreateIterator();

            Console.WriteLine("Duyệt tập hợp phần tử:");
            object item = i.First();
            while (item != null)
            {
                Console.WriteLine($" -> {item}");
                item = i.Next();
            }

            Console.ReadLine();
        }
    }
}