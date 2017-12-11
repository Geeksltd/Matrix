*Namespace: **Olive***
# Boolean.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are two input parameters values.

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| true | .CompareTo(true)  | ➜ 0 | When 'this' value is the same as the CompareTo() parameter, the result is zero
| true | .CompareTo(false)  | ➜ 1 | When 'this' value is not the same as the CompareTo() parameter, the result is one
| false | .CompareTo(true)  | ➜ -1 | 
| false | .CompareTo(false)  | ➜ 0 | 
