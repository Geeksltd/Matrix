*Namespace: **Olive***
# Boolean.CompareTo() ➜ *returns Int32*
Returns a number indicating whether this boolean value is the same (0), smaller than (-1) or larger than (+1) another value.

## Normal Scenarios

|Object|Call|Result|Remarks|
|---|---|---|---|
| true | .CompareTo(false)  | ➜ 1 | True is considered bigger than False. So the result is 1.
| true | .CompareTo(true)  | ➜ 0 | True is the same as the True. So the result is 0.
| false | .CompareTo(true)  | ➜ -1 | False is considered smaller than True. So the result is 1.
| false | .CompareTo(false)  | ➜ 0 | Again, False is the same as False.

## Special Scenarios
CompareTo() has another overload which takes an "Object" as input.

|Object|Call|Result|Remarks|
|---|---|---|---|
| true | .CompareTo(null)  | ➜ 1 |  True is considered bigger than NULL. 
| false | .CompareTo(null)  | ➜ ???? |  ***TODO: Test.***
| false | .CompareTo(*{"AnyOtherObjectType"}*)  | ➜ ArgumentException| The actual value of the object parameter should be either a boolean, or null.
| false | .CompareTo(*(bool?)true*)  | ➜ ???? |  ***TODO: Test Nullable bool.***