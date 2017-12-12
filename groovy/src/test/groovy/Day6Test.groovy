import spock.lang.Specification

class Day6Test extends Specification {

    def "calculates cycles"() {
        when:
        Day6 app = new Day6()

        then:
        app.countCycles([0, 2, 7, 0]) == 5
    }

}
