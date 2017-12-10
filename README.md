# .NET Cheat Sheet
A quick cheat sheet for all commonly used .NET Apis.

For each method or property, it provides one MD file with all the information you need to use it:
- Simple description
- Examples
- Edge cases

They can be looked up right inside Visual Studio using the Cheat Visual Studio extension when using each method.

## Path Structure
Each method, property or event, should be created as a .MD file with the following exact path:
{Namespace}/{Class}/{Member}.md

For example the Append() method of the StringBuilder class will be documented here:
> System/Text/StringBuilder/Append.md

## Content template
```
*Namespace: **{Namespace}***
# {ClassName}.{MethodName}(*params*) ➜ *returns {ReturnValue}*
{Description}
## Remarks
- {Tip 1}
- {Tip 2}
- ...

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}
| ...  | .{MethodName}({param})  | ➜ {Result} | {Example notes (optional)}

[More details](https://docs.microsoft.com/en-us/dotnet/api/{namespace}.{class}.{member})
```

## Editing guidelines
- The top description should be focused on the high level reason why that thing exists. That helps the learner to quickly grasp what this member is, without worrying about the details of HOW it's implemented.
- All further details about how it's implemented, or tips should go under Remarks.
- Under *examples*, make sure you include edge cases. Often a learner needs to know how the method handles special cases such as null, zero, negative value, etc. This will help them to avoid writing unnecessary validation logic in their code if the target method can already handle it nicely.
- Give as much attention to the examples as possible. That's the main way the learner will use to understand the method. Use easy to follow and simple examples.
