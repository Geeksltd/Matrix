*Namespace: **Olive***
# Boolean.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are two input parameters values.

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| true | .CompareTo(true)  | ➜ 0 | When 'this' value is the same as the CompareTo() parameter, the result is zero
| true, false | .CompareTo(true,false)  | ➜ 1 | first value does not equal to second one
| null, false | .CompareTo(null,false)  | ➜ 1 | first value is null and second value is false
| null, true | .CompareTo(null,true)  | ➜ -1 | first value is null and second value is true,
| true, null | .CompareTo(true,null)  | ➜ 1 | second value is null and first value is true
| false, null | .CompareTo(false,null)  | ➜ -1 | second value is null and first value is false
| null, null | .CompareTo(null,null)  | ➜ 0 | both values equal to null
