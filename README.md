The **.NET ObjectFiller** fills the properties of your .NET objects with random data!
It has a very comfortable Fluent API.
You are able to fill object instances or you just let them create for you. 
It is possible to create instances of classes which have constructors with parameters.
You can also fill properties which are a interface.
The **.NET ObjectFiller** also supports Lists and Dictionaries.

##Where can i get it?
Easy, you can find it at nuget! It's just one DLL!

**Look here:** https://www.nuget.org/packages/Tynamix.ObjectFiller


##For what do you need it?

You can use the **.NET ObjectFiller** for generating test data for your **UnitTests** or for your **DesignViewModels** in WPF or for whatever you need some random testdata.
I will show you some examples how you can work with it.

##Can i extend the .NET ObjectFiller

The **.NET ObjectFiller** is very flexible and easy to extend. With the help of the **FluentAPI** you can configure and extend the ObjectFiller. In the examples i will show you how to do it.

##Examples

###Let's start easy

```csharp
public class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime Birthday { get; set; }
}

public class HelloFiller
{
    public void FillPerson()
    {
        ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
        Person p = pFiller.Fill();
    }
}
```

Nothing special, it will just create a instance of a **```Person```** and fill it with some random data.

###Let's use the fluent setup API

```csharp
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForType<string>(() => "SomeString")
                .RandomizerForType<DateTime>(() => new DateTime(2014, 4, 1));
            Person p = pFiller.Fill();
        }
    }
```

So what does this do? First you say **```pFiller.Setup()```**. With **```.Setup()```** you start configure the ObjectFiller. In this example we say to the ObjectFiller: Hey ObjectFiller, whenever there will be a property of type **```string```**, just fill it with the word "SomeString". And when there is a **```DateTime```** set it always to 1.4.2014! Easy! Isn't it? But it will get cooler!

```csharp
  public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForProperty(() => "John", p => p.Name)
                .RandomizerForProperty(new RealNamePlugin(false, true), p => p.LastName);

            Person filledPerson = pFiller.Fill();
        }
    }
```

Here we say: Ok ObjectFiller, fill the property **```Name```** of a **```Person```** with the value "John" and fill the property **```LastName```** with some random real lastname. The **```RealNamePlugin```** is a plugin which is written for the ObjectFiller and comes with the ObjectFiller.DLL.
Its also really easy to write a plugin by yourself. I will show you that later.

###Ignore Properties

```csharp
     public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .IgnoreProperties(p => p.LastName, p => p.Name);

            Person filledPerson = pFiller.Fill();
        }
    }
```

With **```IgnoreProperties```** you can exclude properties to not generate random data for it. When we will now fill a person, all properties get filled except **```LastName```** and **```Name```**.

###Setup Subtypes

```csharp
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public Address Address { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForProperty(() => "John", x => x.Name)
                .SetupFor<Address>()
                .RandomizerForProperty(() => "Dresden", x => x.City);


            Person filledPerson = pFiller.Fill();
        }
    }
```

With **```SetupFor<T>```** you start a setup for another type. In the example above we define that the ```Name``` of the ```Person``` will be "John" and the ```City``` of an ```Address```-object will be "Dresden". **```SetupFor<T>```** takes an ```bool``` parameter. if this is set to **```true```** than all the settings which was made to the parent type will be set back to default. When its not set or false, then the setup will take the setup of the parenttype, except the settings which are made specially for this actual type.

###Fill objects with constructor arguments

```csharp
    public class Person
    {
        public Person(Address address)
        {
            Address = address;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public Address Address { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .IgnoreProperties(p => p.Address);

            Person filledPerson = pFiller.Fill();
        }
    }
```

With ObjectFiller.NET it is also possible to **instantiate** objects which have a **constructor WITH parameters**. 
In the setup i ignore the property **```Address```** of the person because it will be set in the constructor. 
After call  **```.Fill()```** the property is filled because it is set in the constructor!

Now lets do something really cool.

###Fill Interface-Properties

```csharp
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public IAddress Address { get; set; }
    }

    public class Address : IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    public interface IAddress { }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>();

            Person filledPerson = pFiller.Fill();
        }
    }
```

You see? The **```Person```** has now an **```Address```**. But wait? It's an **```IAddress```**! An **interface**? 
YES! And ObjectFiller can handle that. Just say **```RegisterInterface```** and give the ObjectFiller the information what is the concrete implementation for that interface. Nice huh?

###Fill Lists and Dictionaries

```csharp
    public class Person
    {
        public Dictionary<string, List<Address>> SenselessDictionary { get; set; }
        public List<IAddress> SenselessList { get; set; }
    }

    public class Address : IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    public interface IAddress {  }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>();

            Person filledPerson = pFiller.Fill();
        }
    }
```

It is also really easy possible to fill **```Dictionary```** and **```Lists```** objects.

###Mix all up

```csharp
    public class Person
    {
        public Person(IAddress address)
        {
            Address = address;
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public IAddress Address { get; set; }

        public Dictionary<string, List<Address>> SenselessDictionary { get; set; }
        public List<IAddress> SenselessList { get; set; }
    }

    public class Address : IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    public interface IAddress { }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .RandomizerForProperty(new RealNamePlugin(true, false), p => p.LastName, p => p.Name)
                .RandomizerForProperty(() => new Random().Next(10, 32), p => p.Age)
                .SetupFor<Address>()
                .RandomizerForProperty(new MnemonicStringPlugin(1), a => a.City)
                .IgnoreProperties(a => a.Street);

            Person filledPerson = pFiller.Fill();
        }
    }
```

**Now let us mix all up!** What happens here? Well, we say: Ok ObjectFiller, the **```IAddress```** interface will be implemented by the **```Address```** class.
The **```Name```** and **```LastName```** of a person will be generated by the **```RealNamePlugin```**. The age of the person should be something between 10 and 32. 
When you generate a city use the **```MnemonicStringPlugin```** and finally ignore the Street in the **```Address```** and don't fill it. Ok thats a lot. But it works!

##Available Plugins

The ObjectFiller.NET is easy to extend and you can write your own plugins for it.
There are several plugins already implemented which are documented below.

###MnemonicStringPlugin

The **```MnemonicStringPlugin```** generates mnemonic words. A mnemonic word is a word with every second letter is a vowel. For example: Dubinola
The advantage is that these words are mostly easy to pronounce.

```csharp
    public class Person
    {
        public string Name { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForType<string>(new MnemonicStringPlugin(1, 5, 10));

            Person filledPerson = pFiller.Fill();
        }
    }
```

In this example we see how to use the **```MnemonicStringPlugin```**. It has three constructor parameters. The first one defines how much words will be generated. The second is the word min length and the last one is the word max length.

###RealNamePlugin

The **```RealNamePlugin```** is made to generate strings based on real names like "Jennifer" or "Miller". The realname plugin knows about 5000 First- and Lastnames. 

```csharp
    public class Person
    {
        public string Name { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForType<string>(new RealNamePlugin(true, false));

            Person filledPerson = pFiller.Fill();
        }
    }
```

The **```RealNamePlugin```** has up to two constructor parameters. They define if you want to generate a FirstName, a LastName or the FullName

###RandomListItem - Plugin

The **```RandomListItem```** plugin is usefull when you want to setup a predefined set of values which are possible to use. The **```RandomListItem```** will pick a random one.

```csharp
    public class Person
    {
        public string Name { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            List<string> allNames = new List<string>() { "Jennifer", "Jenny", "Tom", "John" };

            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForType<string>(new RandomListItem<string>(allNames));

            Person filledPerson = pFiller.Fill();
        }
    }
```

In the example u can see that i set up four names. One of these will be the name of the **```Person```** object.

###PatternGenerator Plugin

The **```PatternGenerator```** can be used to created strings following a pattern.

```csharp
    public void FillPerson()
    {
        ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
        
		pFiller.Setup()
			.RegisterInterface<IAddress, Address>()
			.SetupFor<Address>()
			.RandomizerForProperty(new PatternGenerator("{A}{a:2-8}"), x => x.City)
			.RandomizerForProperty(new PatternGenerator("CA {C:10000}"), x => x.PostalCode)
			.RandomizerForProperty(new PatternGenerator("Main Street {C:100,10} NE"), x => x.Street);
				
        Person filledPerson = pFiller.Fill();
    }
```


Address.City will become a string, starting with one upper-case char, followed by 2..8 lower-case chars.
Address.PostalCode will start with the fixed value "CA ", followed by a number starting at 10000, incremented by 1 in the next address in the persons address list.
The Main Street will include a number starting at 100, incremented by 10.

The pattern generator can be extended, to allow combining built-in expressions and custom expressions within a pattern.

```csharp
    public class FrenchUnicodeExpressionGenerator : IExpressionGenerator<string>
    {
        public static IExpressionGenerator TryCreateInstance(string expression)
        {
 			if (expression == "{U:fr}")
				return new FrenchUnicodeExpressionGenerator();
			else 
				return null;
       }
       
       public string GetValue()
       {    
            return "Bonjour";
       }
    }
    
    public void FillPerson()
    {
        PatterGenerator.ExpressionGeneratorFactories.Add(FrenchUnicodeExpressionGenerator.TryCreateInstance);
    
        ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
        
		pFiller.Setup()
			.RegisterInterface<IAddress, Address>()
			.SetupFor<Address>()
			.RandomizerForProperty(new PatternGenerator("{C}x {U:fr}"), x => x.Street);
    }
```


###Write your own plugin

To write your own plugin is very easy.
Just implement the **``` IRandomizerPlugin<T> ```** plugin. The typeparamer **```T```** defines for which type you will write the plugin. The interface just has one function which you have to implement: **```T GetValue();```**
Thats all!
You can write plugins for simple types and complex types.

Here is a very simple example:

```csharp
  public class MyFirstPlugin : IRandomizerPlugin<string>
    {
        public string GetValue()
        {
            List<string> allNames = new List<string>() { "Jennifer", "Jenny", "Tom", "John" };
            Random r = new Random();

            return allNames[r.Next(0, allNames.Count)];
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RandomizerForType<string>(new MyFirstPlugin());

            Person filledPerson = pFiller.Fill();
        }
    }
```

**```MyFirstPlugin```** does basically the same thing as the **```RandomListItem```**-plugin, but it is a good way to show you how easy it is to implement your own plugin.

##Thank you for using ObjectFiller.NET

If you have any questions or found bugs, have ideas for improvements, feel free to contact me!
