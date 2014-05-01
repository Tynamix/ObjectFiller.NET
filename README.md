#ObjectFiller.NET
<img align="left" src="https://raw.githubusercontent.com/Tynamix/ObjectFiller.NET/master/logo.png" alt="Logo" />
The **.NET ObjectFiller** fills the properties of your .NET objects with random data!
It has a very comfortable Fluent API.
You are able to fill object instances or you just let them create for you. 
It is possible to create instances of classes which have constructors with parameters.
You can also fill properties which are derived by a interface.
The **.NET ObjectFiller** also supports IEnumerable<T> (and all derivations) and Dictionaries.

##Table of contents
 - [Where can i get it?](#where-can-i-get-it)
 - [For what do you need it?](#for-what-do-you-need-it)
 - [Can i extend the .NET ObjectFiller?](#can-i-extend-the-net-objectfiller)
 - [Examples](#examples)
   - [Let's start easy](#lets-start-easy)
   - [Let's use the fluent setup API](#lets-use-the-fluent-setup-api)
   - [Ignore Properties](#ignore-properties)
   - [Setup Subtypes](#setup-subtypes)
   - [Fill objects with constructor arguments](#fill-objects-with-constructor-arguments)
   - [Fill Interface-Properties](#fill-interface-properties)
   - [Fill Lists and Dictionaries](#fill-lists-and-dictionaries)
   - [Mix all up](#mix-all-up)
 - [Available Plugins](#available-plugins)
   - [RangeIntegerPlugin](#rangeintegerplugin)
   - [MnemonicStringPlugin](#mnemonicstringplugin)
   - [RealNamePlugin](#realnameplugin)
   - [RandomListItem Plugin](#randomlistitem---plugin)
   - [PatternGenerator Plugin](#patterngenerator-plugin)
   - [Lorem Ipsum String Plugin](#lorem-ipsum-string-plugin)
 - [Write your own plugin](#write-your-own-plugin)
 - [Thank you](#thank-you-for-using-objectfillernet)
   

##Where can i get it?
Easy, you can find it at nuget! It's just one DLL!
**Look here:** https://www.nuget.org/packages/Tynamix.ObjectFiller


##For what do you need it?

You can use the **.NET ObjectFiller** for generating test data for your **UnitTests** or for your **DesignViewModels** in WPF or for whatever you need some random testdata.
I will show you some examples how you can work with it.

**The ObjectFiller.NET ...:**
*   ...fill the public writable properties of your objects
*   ...fills also all subobjects
*   ...has a nice FluentAPI
*   ...can handle constructor with parameters
*   ...can handle IEnumerable<T> and all derivations
*   ...can handle Interfaces
*   ...cas handle Dictionaries
*   ...is highly customizable
*   ...has many nice plugins
*   ...is very easy to extend

##Can i extend the .NET ObjectFiller?

Of course! The **.NET ObjectFiller** is very flexible and easy to extend. With the help of the **FluentAPI** you can configure and extend the ObjectFiller. You are also able to [write your own **Plugin**](#write-your-own-plugin)! In the examples i will show you how to do it. 

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
            Filler<Person> pFiller = new Filler<Person>();
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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(() => "SomeString")
                .OnType<DateTime>().Use(() => new DateTime(2014, 4, 1));
            Person p = pFiller.Fill();
        }
    }
```

So what does this do? First you say **```pFiller.Setup()```**. With **```.Setup()```** you start configure the ObjectFiller. With **```OnType<T>()```** you define which type will be configured and with **```.Use( ... )```** you define what the objectfiller should do with the type. You are able to write your own **```.Func<T>()```** or implement a **```IRandomizerPlugin<T>```** or just use one which is already implemented. ObjectFiller is very flexible and easy to extend!
In this example we say to the ObjectFiller: Hey ObjectFiller, whenever there will be a property of type **```string```**, just fill it with the word "SomeString". And when there is a **```DateTime```** set it always to 1.4.2014! Easy! Isn't it? But it will get cooler!

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(p=>p.Name).Use(() => "John")
                .OnProperty(p => p.LastName).Use(new RealNames(false, true));

            Person filledPerson = pFiller.Fill();
        }
    }
```

Here we say: Ok ObjectFiller, fill the property **```Name```** of a **```Person```** with the value "John" and fill the property **```LastName```** with some random real lastname. The **```.OnProperty```**-method works very similar to the **```OnType<T>()```** method! With **```.Use(new RealNames(false, true));```** we use a **```RealNamePlugin```**.
The **```RealNamePlugin```** is a plugin which is written for the ObjectFiller and comes with the ObjectFiller.DLL.
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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(x=>x.LastName, x=>x.Name).IgnoreIt();

            Person filledPerson = pFiller.Fill();
        }
    }
```

With **```.IgnoreIt()```** you can exclude properties to not generate random data for it. When we will now fill a person, all properties get filled except **```LastName```** and **```Name```**.

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().IgnoreIt();

            Person filledPerson = pFiller.Fill();
        }
    }
```

The same method **```.IgnoreIt()```** is also available after you call **```.OnType<T>()```** for types. With that it is possible to exclude all properties of a specific type. When we will now fill a person, all properties get filled except **```LastName```** and **```Name```** because they are of type **```string```**.

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(x => x.Name).Use(() => "John")
                .SetupFor<Address>()
                .OnProperty(x => x.City).Use(() => "Dresden");

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(x=>x.Address).IgnoreIt();

            Person filledPerson = pFiller.Fill();
        }
    }
```

With ObjectFiller.NET it is also possible to **instantiate** objects which have a **constructor WITH parameters**. 
In the setup i ignore the property **```Address```** of the person because it will already been set in the constructor. 

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>();

            Person filledPerson = pFiller.Fill();
        }
    }
```

You see? The **```Person```** has now an **```Address```**. But wait? It's an **```IAddress```**! An **interface**? 
YES! And ObjectFiller can handle that. Just say **```.Register<T>()```** after you called **```.OnType<T>()```** and give the ObjectFiller the information what is the concrete implementation for that interface. Nice huh?

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

    public interface IAddress { }

    public class HelloFiller
    {
        public void FillPerson()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>();

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnProperty(p => p.LastName, p => p.Name).Use(new RealNames(true, false))
                .OnProperty(p => p.Age).Use(() => new Random().Next(10, 32))
                .SetupFor<Address>()
                .OnProperty(p => p.City).Use(new MnemonicString(1))
                .OnProperty(x => x.Street).IgnoreIt();

            Person filledPerson = pFiller.Fill();
        }
    }
```

**Now let us mix all up!** What happens here? Well, we say: Ok ObjectFiller, the **```IAddress```** interface will be implemented by the **```Address```** class.
The **```Name```** and **```LastName```** of a person will be generated by the **```RealNamesPlugin```**. The age of the person should be something between 10 and 32. 
When you generate a city use the **```MnemonicStringPlugin```** and finally ignore the Street in the **```Address```** and don't fill it. Ok thats a lot. But it works!

##Available Plugins

The ObjectFiller.NET is easy to extend and you can write your own plugins for it.
There are several plugins already implemented which are documented below.

###RangeIntegerPlugin

The **```RangeIntegerPlugin```** is a very easy plugin and generates integers in a given range.
It has upto two constructor parameter. The first one is the maximum value and the second one (optional) the minimum. When minimum is not set, the minimum will be 0!

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<int>().Use(new Range(20, 79));

            Person filledPerson = pFiller.Fill();
        }
    }
```

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(new MnemonicString(1,5,10));
                
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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(new RealNames(true, false));

            Person filledPerson = pFiller.Fill();
        }
    }
```

The **```RealNamePlugin```** has up to two constructor parameters. They define if you want to generate a FirstName, a LastName or the FullName

###RandomListItem - Plugin

The **```RandomListItem```** plugin is usefull when you want to setup a predefined set of values which are possible to use. The **```RandomListItem```** will then pick a random one from the list.

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

            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(new RandomListItem<string>(allNames));

            Person filledPerson = pFiller.Fill();
        }
    }
```

In the example u can see that i set up four names. One of these will be the name of the **```Person```** object.

###PatternGenerator Plugin

The **```PatternGenerator```** can be used to created strings following a pattern.

```csharp
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public List<Address> Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup()
                .SetupFor<Address>()
                .OnProperty(x => x.City).Use(new PatternGenerator("{A}{a:2-8}"))
                .OnProperty(x => x.PostalCode).Use(new PatternGenerator("CA {C:10000}"))
                .OnProperty(x => x.Street).Use(new PatternGenerator("Main Street {C:100,10} NE"));

            Person filledPerson = pFiller.Fill();
        }
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
    
        Filler<Person> pFiller = new Filler<Person>();
        
		pFiller.Setup()
			.OnType<IAddress>().Register<Address>()
			.SetupFor<Address>()
			.OnProperty(x => x.Street).Use(new PatternGenerator("{C}x {U:fr}"));
    }
```

###Lorem Ipsum String Plugin

The "Lorem Ipsum" plugin generates some random text which contains the famous "Lorem Ipsum" text. Read more about the Lorem Ipsum [here](http://en.wikipedia.org/wiki/Lorem_ipsum)

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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(new LoremIpsum(500));

            Person filledPerson = pFiller.Fill();
        }
    }
```
This example generates a Lorem Ipsum text with 500 words for all ```string``` properties of the person.

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
