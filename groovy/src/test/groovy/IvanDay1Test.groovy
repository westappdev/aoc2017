import spock.lang.Specification

class IvanDay1Test extends Specification {

    String PATH = "src/test/resources/TestInput.txt"

    def "can read input"() {
        expect:
        new IvanDay1().input(PATH) == "Hello world !"
    }

    def "calculate the the sum of consecutive matching digits"() {
        setup:
        def a = new IvanDay1()

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
