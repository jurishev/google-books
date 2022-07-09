export interface Volume {
    id: string,
    selfLink: string,
    volumeInfo: {
        title: string,
        authors: string[],
        publisher: string,
        publishedDate: string,
        description: string,
        categories: string[],
        previewLink: string,
        imageLinks: {
            smallThumbnail: string,
            thumbnail: string
        }
    }
}
