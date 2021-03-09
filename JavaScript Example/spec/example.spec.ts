import * as fc from 'fast-check';

module
function indexOf(text: string, pattern: string): number {
    return text.indexOf(pattern);
}

describe("a suite", () => {
    it("contains a spec with an expectation", () => {
        expect(true).toBe(true)
    })
})

describe('stuff', () => {
    it('bad example', () => {
        const i = indexOf("!", "!")
        expect(i).toBe(0)
    })
})

describe('property based tests', () => {
    it('should always contain itself', () => {
        fc.assert(fc.property(fc.string(), (text: string) => indexOf(text, text) !== -1));
    });
    xit('should always contain substrings', () => {
        fc.assert(
            fc.property(fc.string(), fc.string(), fc.string(),
                (a: string, b: string, c: string) => {
                    return indexOf(b, a + b + c) !== -1;
                })
        )
    })
})