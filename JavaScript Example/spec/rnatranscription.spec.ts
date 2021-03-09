import fc from "fast-check"

function transcribe(dna: string): string {
    return dna.split("")
        .map(c => {
            if (c == "T") {
                return "A"
            } else if (c == "A") {
                return "U"
            } else if (c == "G") {
                return "C"
            } else if (c == "C") {
                return "G"
            }
            return c
        })
        .join("")
}

describe('Rna Transcription', () => {
    it("An empty string transposes to an empty string", () => {
        expect(transcribe("")).toBe("")
    })

    it("RNA is the same length as the DNA", () => {
        fc.assert(
            fc.property(

                fc.stringOf(fc.oneof(fc.constant("A"), fc.constant("C"), fc.constant("G"), fc.constant("T"))),
                (a: string) => {
                    return transcribe(a).length == a.length
                }
            )
        )
    })
    it("Dna T Count Matches The Rna A count", () => {
        fc.assert(
            fc.property(
                fc.stringOf(fc.oneof(fc.constant("A"), fc.constant("C"), fc.constant("G"), fc.constant("T"))),
                (a: string) => {
                    const expected = a.split("").filter(c => c == "T").length
                    return transcribe(a).split("").filter(c => c == "A").length == expected
                }
            )
        )
    })
    it("Dna A Count Matches The Rna U count", () => {
        fc.assert(
            fc.property(
                fc.stringOf(fc.oneof(fc.constant("A"), fc.constant("C"), fc.constant("G"), fc.constant("T"))),
                (a: string) => {
                    const expected = a.split("").filter(c => c == "A").length
                    return transcribe(a).split("").filter(c => c == "U").length == expected
                }
            )
        )
    })
    it("Dna G Count Matches The Rna C count", () => {
        fc.assert(
            fc.property(
                fc.stringOf(fc.oneof(fc.constant("A"), fc.constant("C"), fc.constant("G"), fc.constant("T"))),
                (a: string) => {
                    const expected = a.split("").filter(c => c == "G").length
                    return transcribe(a).split("").filter(c => c == "C").length == expected
                }
            )
        )
    })
    it("RNA only contains CGA or U", () => {
        fc.assert(
            fc.property(
                fc.stringOf(fc.oneof(fc.constant("A"), fc.constant("C"), fc.constant("G"), fc.constant("T"))),
                (a: string) => {
                    const rna = transcribe(a)
                    const pattern = new RegExp("^[CGAU]*$")
                    return pattern.test(rna)
                }
            )
        )
    })
})