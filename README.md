#ObjectFiller.NET
<img align="left" src="https://raw.githubusercontent.com/Tynamix/ObjectFiller.NET/master/logo.png" alt="Logo" />
The **.NET ObjectFiller** fills the properties of your .NET objects with random data!
It has a very comfortable Fluent API.
You are able to fill object instances or you just let them create for you. 
It is possible to create instances of classes which have constructors with parameters.
You can also fill properties which are derived by a interface.
The **.NET ObjectFiller** also supports IEnumerable<T> (and all derivations) and Dictionaries.

##Table of contents
 - [Where can i get it?](#get-it-on-nuget)
 - [For what do you need it?](#for-what-do-you-need-it)
 - [Can i extend the .NET ObjectFiller?](#can-i-extend-the-net-objectfiller)
 - [Examples](#examples)
   - [Let's start easy](#lets-start-easy)
   - [Let's use the fluent setup API](#lets-use-the-fluent-setup-api)
   - [Export ObjectFiller Settings](#export-objectfiller-settings)
   - [Ignore Properties](#ignore-properties)
   - [Setup Subtypes](#setup-subtypes)
   - [Fill objects with the IEnumerable interface](#fill-objects-with-the-ienumerable-interface)
   - [Fill objects with constructor arguments](#fill-objects-with-constructor-arguments)
   - [Fill Interface-Properties](#fill-interface-properties)
   - [Fill Lists and Dictionaries](#fill-lists-and-dictionaries)
   - [Detect Circular Dependencies](#detect-circular-dependencies)
   - [Mix all up](#mix-all-up)
 - [Available Plugins](#available-plugins)
   - [IntRangePlugin](#rangeintegerplugin)
   - [MnemonicStringPlugin](#mnemonicstringplugin)
   - [RealNamePlugin](#realnameplugin)
   - [RandomListItem Plugin](#randomlistitem---plugin)
   - [PatternGenerator Plugin](#patterngenerator-plugin)
   - [Lipsum String Plugin](#lipsum-string-plugin)
   - [Sequence Generator Plugin](#sequencegenerator-plugins)
 - [Write your own plugin](#write-your-own-plugin)
 - [Thank you](#thank-you-for-using-objectfillernet)
   

##Get it on NuGet!
###https://www.nuget.org/packages/Tynamix.ObjectFiller


##For what do you need it?

You can use the **.NET ObjectFiller** for generating test data for **Prototyping**, for your **UnitTests**, for generating data of your **Mocks*** or for your **DesignViewModels** in WPF or wherever you need some random testdata.
I will show you some examples how you can work with it.

**The ObjectFiller.NET ...:**
*   ...fill the public properties of your objects - even with private setters
*   ...fills also all subobjects
*   ...has a nice FluentAPI
*   ...handles constructors with parameters
*   ...handles IEnumerable<T> and all derivations
*   ...handles Dictionaries
*   ...handles Interfaces
*   ...handles Enumerations
*   ...is highly customizable
*   ...has many nice plugins
*   ...is very easy to extend

##Can I extend the .NET ObjectFiller?

Of course! The **.NET ObjectFiller** is very flexible and easy to extend. With the help of the **FluentAPI** you can configure and extend the ObjectFiller. You are also able to [write your own **Plugin**](#write-your-own-plugin)! In the examples I will show you how to do it. 

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
	public void CreatePerson()
	{
	    Filler<Person> pFiller = new Filler<Person>();
	    Person p = pFiller.Create();
	}
}
```

Nothing special, it will just create a instance of a **```Person```** and fill it with some random data.

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
        Person person = new Person();

        Filler<Person> pFiller = new Filler<Person>();
        Person p = pFiller.Fill(person);
    }
}
```

It is also possible to fill an already existing instance of an object. In the example we first create a person and then call ```Fill(...)``` instead of ```Create()```. This is great for stuff like DesignViewModels in WPF or whereever you need to fill the object in the constructor with ```Fill(this)``` for example.

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
            Person p = pFiller.Create();
        }
    }
```

So what does the above code do? First you say **```pFiller.Setup()```**. With **```.Setup()```** you start configure the ObjectFiller. With **```OnType<T>()```** you define which type will be configured and with **```.Use( ... )```** you define what the Objectfiller should do with the type. You are able to write your own **```.Func<T>()```** or implement a **```IRandomizerPlugin<T>```**, or just use one of the provided plugins. ObjectFiller is very flexible and easy to extend!
In this example we say to the ObjectFiller: Hey ObjectFiller, whenever there will be a property of type **```string```**, just fill it with the word "SomeString". And when there is a **```DateTime```** set it always to 1.4.2014! Easy! Isn't it? But it will get even cooler!

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

            Person filledPerson = pFiller.Create();
        }
    }
```

Here we say: Ok ObjectFiller, fill the property **```Name```** of a **```Person```** with the value "John" and fill the property **```LastName```** with some random real lastname. The **```.OnProperty```** method works very similar to the **```OnType<T>()```** method! With **```.Use(new RealNames(false, true));```** we use a **```RealNamePlugin```**.
The **```RealNamePlugin```** is a plugin which comes with the ObjectFiller assembly along.
Its also really easy to write a plugin by yourself. I will show you that later.

###Export ObjectFiller Settings

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
            private FillerSetup _fillerSetup;
            public HelloFiller()
            {
                Filler<Person> pFiller = new Filler<Person>();

                _fillerSetup = pFiller.Setup()
                      .OnProperty(x => x.LastName).Use(new RealNames(RealNameStyle.LastNameOnly))
                      .OnProperty(x => x.Name).Use(new RealNames(RealNameStyle.FirstNameOnly))
                      .OnType<int>().Use(new IntRange(18, 75))
                      .Result;


            }
            public void FillPerson()
            {
                Filler<Person> pFiller = new Filler<Person>();
                pFiller.Setup(_fillerSetup);

                Person filledPerson = pFiller.Create();
            }
        }
```
Here we can see that i created the filler setup in the constructor and save the ```.Result``` of the filler setup to a private field. In the method ```FillPerson()``` we call the ```.Setup(_fillerSetup)``` with the setup of this private field! Thats good if you want to reuse your setup!


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

            Person filledPerson = pFiller.Create();
        }
    }
```

With **```.IgnoreIt()```** you can exclude properties from randomizing, they will keep their default value instead. When we will now fill a person, all properties get filled except **```LastName```** and **```Name```**.

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

            Person filledPerson = pFiller.Create();
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

            Person filledPerson = pFiller.Create();
        }
    }
```

With **```SetupFor<T>```** you start a setup for another type. In the example above we define that the ```Name``` of the ```Person``` will be "John" and the ```City``` of an ```Address``` object will be "Dresden". **```SetupFor<T>```** takes an ```bool``` parameter. If this is set to **```true```** then all the settings which were made on the parent type will be set back to default. When a property is not set up, then the filler will take the setup of the parent type, except the settings which are made specially for this actual type.

###Fill objects with the IEnumerable interface

With ObjectFiller.NET you can use the IEnumerable interface to fill objects. Use it for example when you want to fill a property in a specific order with include and exclude and all the other cool stuff which IEnumerable and LINQ supports.

```csharp
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .SetupFor<Address>()
                .OnProperty(x => x.Id).Use(Enumerable.Range(1, 100).Where(x => x % 2 == 0));

            Person filledPerson = pFiller.Create();
        }
    }
```
In this example the ID of an Address item will be an even number between 1 and 100 in ascending order.
This means, the first Address will have the Id 2, the second the Id 4, the fourth Id 6 and so...

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

            Person filledPerson = pFiller.Create();
        }
    }
```

With ObjectFiller.NET it is also possible to **instantiate** objects which have a **constructor WITH parameters**. 
In the above setup I ignore the **```Address```** property. 

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

            Person filledPerson = pFiller.Create();
        }
    }
```

You see? The **```Person```** has now an **```Address```**. But wait? It is an **```IAddress```**! An **interface**? 
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
                .OnType<IAddress>().CreateInstanceOf<Address>();

            Person filledPerson = pFiller.Create();
        }
    }
```

It is also really easy possible to fill **```Dictionary```** and **```Lists```** objects.

###Detect Circular Dependencies

The ObjectFiller is able to detect circulare references. You can specify that the ObjectFiller will ignore the circular references or throw an exception when a circular reference occurs.

```csharp

    public class Children
    {
        public Parent Parent { get; set; }
    }

    public class Parent
    {
        public List<Children> Childrens { get; set; }
    }

    public class HelloFiller
    {
        public void FillPerson()
        {
            Filler<Parent> pFiller = new Filler<Parent>();
            pFiller.Setup().OnCircularReference().ThrowException(false);

            Parent filledParent = pFiller.Create();
        }
    }
```
Here you can see that a parent has a ```List<Children>``` and the ```Children``` has a ```Parent``` - a circular reference. The ObjectFiller detects that and doesn't fill the circular object anymore! When you call ```.Setup().OnCircularReference().ThrowException(true);``` with the ```true``` flag it will raise an exception instead of just ignore the circular reference.

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
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(p => p.LastName, p => p.Name).Use(new RealNames(true, false))
                .OnProperty(p => p.Age).Use(() => new Random().Next(10, 32))
                .SetupFor<Address>()
                .OnProperty(p => p.City).Use(new MnemonicString(1))
                .OnProperty(x => x.Street).IgnoreIt();

            Person filledPerson = pFiller.Create();
        }
    }
```

**Now let us mix all up!** What happens here? Well, we say: Ok ObjectFiller, the **```IAddress```** interface will be implemented by the **```Address```** class.
The **```Name```** and **```LastName```** of a person will be generated by the **```RealNamesPlugin```**. The age of the person should be somewhere between 10 and 32. 
When you generate a city use the **```MnemonicStringPlugin```** and finally ignore the Street in the **```Address```**. Quite a lot. But it works!

##Available Plugins

The ObjectFiller.NET is easy to extend, you can write your own plugins for it.
There are several plugins already implemented which are documented below.

###RangeIntegerPlugin

The **```RangeIntegerPlugin```** is a very simple plugin and generates integers in a given range.
It has up to two constructor parameter. The first one is the maximum value and the second one (optional) the minimum. When minimum is not set, the minimum will be 0!

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
                .OnType<int>().Use(new IntRange(20, 79));

            Person filledPerson = pFiller.Create();
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
                
            Person filledPerson = pFiller.Create();
        }
    }
```

In this example we see how to use the **```MnemonicStringPlugin```**. It has three constructor parameters. The first one defines how much words will be generated. The second is the word min length and the last one is the word max length.

###RealNamePlugin

The **```RealNamePlugin```** is made to generate strings based on real names like "Jennifer" or "Miller". The realname plugin contains about 5000 first- and last names. 

```csharp
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }
            public string FullNameReverse { get; set; }
        }

        public class HelloFiller
        {
            public void FillPerson()
            {
                Filler<Person> pFiller = new Filler<Person>();
                pFiller.Setup()
                    .OnProperty(x => x.FirstName).Use(new RealNames(RealNameStyle.FirstNameOnly))
                    .OnProperty(x => x.LastName).Use(new RealNames(RealNameStyle.LastNameOnly))
                    .OnProperty(x => x.FullName).Use(new RealNames(RealNameStyle.FirstNameLastName))
                    .OnProperty(x => x.FullNameReverse).Use(new RealNames(RealNameStyle.LastNameFirstName));

                Person filledPerson = pFiller.Create();
            }
        }
```

The **```RealNamePlugin```** has a ```RealNameStyle``` enumeration as constructor parameter. With that enumeration you are able to define how the generated name should look like.

###RandomListItem - Plugin

The **```RandomListItem```** plugin is usefull when you want to choose the output values from a certain set of values. The **```RandomListItem```** will then pick randomly one item from the list.

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
                .OnType<string>().Use(new RandomListItem<string>("Jennifer", "Jenny", "Tom", "John"));

            Person filledPerson = pFiller.Create();
        }
    }
```

In the example you can see that I set up four values, one of them  will be the generated name of the **```Person```** object.

###PatternGenerator Plugin

The **```PatternGenerator```** can be used to created strings following a certain pattern. The actual pattern documentation can be found in **```PatternGenerator```** documentation.

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

            Person filledPerson = pFiller.Create();
        }
    }
```

Some explanation is in order, I think:
Address.City will become a string, starting with exactly one upper-case char, followed by 2..8 lower-case chars.
Address.PostalCode will start with the fixed value "CA ", followed by a number starting at 10000, incremented by 1 in the next address in the persons address list.
The street property will contain the text "Main Street ", followed by a (street) number starting at 100, incremented by 10.

The pattern generator can be extended, to allow combining built-in expressions and custom expressions within a pattern.

```csharp
    public class FrenchWordExpressionGenerator : IExpressionGenerator<string>
    {
        public static IExpressionGenerator TryCreateInstance(string expression)
        {
 			if (expression == "{F}")
				return new FrenchWordExpressionGenerator();
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
        PatterGenerator.ExpressionGeneratorFactories.Add(FrenchWordExpressionGenerator.TryCreateInstance);
    
        Filler<Person> pFiller = new Filler<Person>();
        
		pFiller.Setup()
			.OnType<IAddress>().Register<Address>()
			.SetupFor<Address>()
			.OnProperty(x => x.Street).Use(new PatternGenerator("{C}x {F}"));
    }
```

###Lipsum String Plugin

The "Lorem Ipsum" plugin generates some random text which contains the famous "Lorem Ipsum" text. Read more about the Lorem Ipsum [here](http://en.wikipedia.org/wiki/Lorem_ipsum). With the Lipsum plugin it is also possible to generate some other random text in english (Child Harold), german (In der Fremde) and french (Le Masque)

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
                .OnProperty(x => x.LastName).Use(new Lipsum(LipsumFlavor.LoremIpsum, 3, 5, minWords: 100))
                .OnProperty(x => x.Name).Use(new Lipsum(LipsumFlavor.InDerFremde));
            Person filledPerson = pFiller.Create();
        }
    }
```
This example generates for the ```Lastname``` a Lorem Ipsum text with 3 paragraphs, min 5 sentences and min 100 words.
On property ```Name``` it generates a text on german.

###SequenceGenerator Plugins

The ObjectFiller contains also tons of sequence generators, like the SequenceGeneratorInt32 or the SequenceGeneratorDateTime. When used without any particular setup, they will simply create an increasing sequence like [1,2,3,...]. Most of these sequence generators can be customized to use a different start value (From), a different increment (Step) or can even wrap around after hitting an end value (To). The Step property can be even set to a negative value to generate a decreasing sequence, like in the example below.

```csharp
    public void Countdown()
    {
        var generator = new SequenceGeneratorInt32 { From = 3, Step = -3 };
        generator.GetValue(); // returns  3
        generator.GetValue(); // returns  0
        generator.GetValue(); // returns -3
        generator.GetValue(); // returns -6
    }
```

###Write your own plugin

Writing your own plugin is very easy.
Just implement the **``` IRandomizerPlugin<T> ```** plugin. The typeparameter **```T```** defines for which type you will write the plugin. The interface just has one function which you have to implement: **```T GetValue();```**
Thats all!
You can write plugins for simple types and complex types.

Here is a very simple example:

```csharp
    public class MyFirstPlugin : IRandomizerPlugin<string>
    {
        private readonly Random r = new Random();
        private readonly List<string> allNames = new List<string>() { "Jennifer", "Jenny", "Tom", "John" };

        public string GetValue()
        {
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
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<string>().Use(new MyFirstPlugin());

            Person filledPerson = pFiller.Create();
        }
    }
```

**```MyFirstPlugin```** does basically the same thing as the **```RandomListItem```** plugin, but it is a good way to show you how easy it is to implement your own plugin.

##Thank you for using ObjectFiller.NET

If you have any questions or found bugs, have ideas for improvements, feel free to contact me!
