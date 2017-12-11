*Namespace: **Olive***
# Boolean.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are two input parameters values.

## Remarks
- If first value equals to second one, it returns 0.
- If first value does not equal to second one, it returns 1.
- If both values equal to null, it returns 0.
- If first value is null and second value is true, it returns -1.
- If first value is null and second value is false, it returns 1.
- If second value is null and first value is true, it returns 1.
- If second value is null and first value is false, it returns -1.

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| true, true | .CompareTo()  | ➜ 0 | 
| true, false | .CompareTo()  | ➜ 1 | 
| null, false | .CompareTo()  | ➜ 1 | 
| null, true | .CompareTo()  | ➜ -1 | 
| null, null | .CompareTo()  | ➜ 0 | 
