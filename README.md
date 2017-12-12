# Background
When writing code, developers need to discover APIs as they go. But often they don't use the official MSDN documentation because:

- The definitions can be written in a complex language.
- Examples are rarely there, and can be unnecessarily long. You have to read a lot of code to see the actual point.
- The examples don't show all the different scenarios of using the method.

# .NET API Matrix
This is a new concept for learning APIs. The philosophy behind it is that *what developers are really interested in* when looking through methods in the .NET API is **clear examples** of all the **differnt scenarios for using it**. That's one's mind often works:

- What are typical, happy scenarios?
- What happens in edge case A?
- What happens in edge case B?
- ...

> This project will provide documentation exactly in that format. It's basically a list of scenarios for using each API element (method, property, etc) driven by examples.

# Visual Studio Integration
A VS extension (work in progress) will **bring up this content** right **where you need it**. It will be integrated with the normal Intellisense in Visual Studio and open a pop-up to show the content right there and then.

![](Docs/Examples.png)

Which will then show you [this in a pop-up window](Docs/System/DateTime/AddDays.md)

# Want to contribute?
If you want to contribute to this project please use the following rules.
It's recommended that you install this Visual Studio Extension used for [editing markdown files](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor).

### Path Structure
For each method or property, it provides one MD file with all the information you need to use it:
They can be looked up right inside Visual Studio using the Cheat Visual Studio extension when using each method.

Each method, property or event, should be created as a .MD file with the following exact path:
{Namespace}/{Class}/{Member}.md

For example the Append() method of the StringBuilder class will be documented at the following address in this repository:
> /Docs/System/Text/StringBuilder/Append.md

### Content template
```
*Namespace: **{Namespace}***
# {ClassName}.{MethodName}(*params*) ➜ *returns {ReturnType}*

*** {Description:::: TODO: Very high level and simple explanation. If possible explain the reason why this method exists.}

## Normal Scenarios

|Object|Call|Result|Remarks|
|---|---|---|---|
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}

## Special Scenarios

|Object|Call|Result|Remarks|
|---|---|---|---|
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}

## Remarks
***TODO: Add tips which cannot be demonstrated with an example here.
- {Tip 1}
- {Tip 2}
- ...
```

### Scenarios (Normal / Special)

- Try to include every possible scenario you can think of. Use easy to follow and simple examples.

- Under *Special Scenarios*, include every possible edge case, null, zero, negative values, .... This will help the learner to avoid writing unnecessary validation logic in their code if the target method can already handle it nicely.

### How to create it?
- Start from the Microsoft .NET API documentation website. See[ example for DateTime.AddDays() method](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays?view=netframework-4.7.1)

- Use it to write the top description. But simplify the wording if you can. Do not include details that are confusing, too deep, or not relevant to simply using it.

- For every part of their *description*, *remarks* and *exceptoins*, define an example, and add that information description or remark as the notes for that particular example.

- Then think about all kinds of edge cases and scenarios which are not covered in the Microsoft docs, and add examples for those too.

> There is a small Console application named **Check** where you can actually test every scenario to ensure your examples documentation is valid.

- If it's not possible or useful to show any particular remark or tip with an example, then add a textl remark item for that. But always favour examples first.


# Why Contribute?
- Deepen and improve your own knowledge of the .NET framework.
- Improve the lives of the the global .NET community and make their lives easier.
- Indirectly improve the quality of software applications across the world.
- Geeks Ltd, a software development company in London, UK, is proud to sponsor this project. We can compensate you for your time.
