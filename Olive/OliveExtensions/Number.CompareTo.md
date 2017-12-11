# Number.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are the values of two input parameters.

## Remarks
- If first value equals to second one, it returns 0.
- If both values equal to null, it returns 0.
- If first value is null and second value is less than 0, it returns -1.
- If first value is null and second value is more than 0, it returns 1.
- If second value is null and first value is less than 0, it returns -1.
- If second value is null and first value is more than 0, it returns 1.

## Examples
|Object|Call|Result|Remarks|
|---|---|---|---|
| -1000, -1000 | .CompareTo()  | ➜ 0 | 
| 0, 0 | .CompareTo()  | ➜ 0 | 
| 0, null | .CompareTo()  | ➜ 1 | 
| null, 0 | .CompareTo()  | ➜ -1 | 
| 1000, -1000 | .CompareTo()  | ➜ 1 | 
| -1000, 1000 | .CompareTo()  | ➜ -1 | 
## Epecial Cases
|Object|Call|Result|Remarks|
|---|---|---|---|
| -1000, 1000000000 | .CompareTo()  | ➜ -1 | 
