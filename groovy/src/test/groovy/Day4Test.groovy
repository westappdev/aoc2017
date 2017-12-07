import spock.lang.Specification

class Day4Test extends Specification {

    def "check the validity of pass phrases"() {

        setup:
        Day4 app = new Day4()

        expect:
        app.valid(phrase) == result

        where:
        phrase            || result
        "aa bb cc dd ee"  || true
        "aa bb cc dd aa"  || false
        "aa bb cc dd aaa" || true
    }


    def "check advanced validity of pass phrases"() {

        setup:
        Day4 app = new Day4()

        expect:
        app.advancedValid(phrase) == result

        where:
        phrase                     || result
        "abcde fghij"              || true
        "abcde xyz ecdab"          || false
        "a ab abc abd abf abj"     || true
        "iiii oiii ooii oooi oooo" || true
        "oiii ioii iioi iiio"      || false
    }
}
