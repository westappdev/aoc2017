import spock.lang.Specification

class Day1Test extends Specification {

    String PATH = "src/test/resources/Day1.txt"

    def "can read input"() {
        expect:
        new Day1().input(PATH) == "Hello world !"
    }

    def "calculate the the sum of consecutive matching digits"() {
        setup:
        def a = new Day1()

        expect:
        a.result(s) == result

        where:
        s          || result
        "1111"     || 4
        "1234"     || 0
        "1122"     || 3
        "91212129" || 9
    }
}
