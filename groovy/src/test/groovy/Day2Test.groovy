import spock.lang.Specification

class Day2Test extends Specification {

    String PATH = "src/test/resources/Day2.txt"

    def "calculate checksum"() {
        when:
        File file = new File(PATH)
        Day2  app = new Day2()

        then:
        app.sChecksum(file) == 18
    }
}
