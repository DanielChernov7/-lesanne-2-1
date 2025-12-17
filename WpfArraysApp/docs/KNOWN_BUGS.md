# Known Bugs

## Bug 1: Price column sorts lexicographically
**Steps**
1. Run the app.
2. Click the `Price` column header to sort ascending.
3. Observe that the car priced at 9500 appears after higher prices.

**Expected**
- Prices are sorted numerically (9500 should appear first).

**Actual**
- Prices are sorted as text, so 9500 is placed after 38900.

**Screenshot**
- `docs/screenshots/bug-price-sorting.png`

## Bug 2: Year search throws on non-numeric input
**Steps**
1. Run the app.
2. Set `Search Field` to `Year`.
3. Type `20a` in the search box.

**Expected**
- The app ignores invalid input or shows a validation message.

**Actual**
- The app throws a runtime exception and closes.

**Screenshot**
- `docs/screenshots/bug-year-search-exception.png`
